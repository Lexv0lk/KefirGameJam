using System;
using System.Collections.Generic;
using Atomic.Objects;
using Game.Scripts.LevelGeneration;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Pools
{
    public class PrefabsPoolSystem : MonoBehaviour
    {
        [SerializeField] private Transform _mainRoot;
        [SerializeField] private int _startCount = 10;

        private Dictionary<LevelPart, LevelPartsPool> _pools = new();
        private Dictionary<LevelPart, LevelPartsPool> _activePools = new();
        private DiContainer _diContainer;

        public event Action<AtomicEntity> Given;
        public event Action<AtomicEntity> Released;

        [Inject]
        private void Init(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public LevelPart Get(LevelPart prefab)
        {
            if (_pools.ContainsKey(prefab) == false)
            {
                GameObject childRoot = new GameObject();
                childRoot.name = $"{prefab.name}_POOL";
                childRoot.transform.parent = _mainRoot;
                var pool = childRoot.AddComponent<LevelPartsPool>();
                
                _diContainer.Inject(pool);
                
                pool.SetInfo(prefab, _startCount, childRoot.transform);
                pool.Initialize();

                _pools[prefab] = pool;
            }

            var instance = _pools[prefab].Get();
            _activePools[instance] = _pools[prefab];
            Given?.Invoke(instance);
            
            return instance;
        }

        public void Release(LevelPart levelPart)
        {
            _activePools[levelPart].ReleaseEntity(levelPart);
            _activePools.Remove(levelPart);
            Released?.Invoke(levelPart);
        }
    }
}