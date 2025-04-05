using System;
using System.Collections.Generic;
using Atomic.Objects;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Pools
{
    public class AtomicPrefabsPoolSystem : MonoBehaviour
    {
        [SerializeField] private Transform _mainRoot;
        [SerializeField] private int _startCount = 10;

        private Dictionary<AtomicEntity, AtomicEntityPool> _pools = new();
        private Dictionary<AtomicEntity, AtomicEntityPool> _activePools = new();
        private DiContainer _diContainer;

        public event Action<AtomicEntity> Given;

        [Inject]
        private void Init(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public AtomicEntity GetEntity(AtomicEntity prefab)
        {
            if (_pools.ContainsKey(prefab) == false)
            {
                GameObject childRoot = new GameObject();
                childRoot.name = $"{prefab.name}_POOL";
                childRoot.transform.parent = _mainRoot;
                var pool = childRoot.AddComponent<InjectedAtomicEntityPool>();
                
                _diContainer.Inject(pool);
                
                pool.SetInfo(prefab, _startCount, childRoot.transform);
                pool.Initialize();

                _pools[prefab] = pool;
            }

            var entity = _pools[prefab].GetEntity();
            _activePools[entity] = _pools[prefab];
            Given?.Invoke(entity);
            
            return entity;
        }

        public void ReleaseEntity(AtomicEntity entity)
        {
            _activePools[entity].ReleaseEntity(entity);
            _activePools.Remove(entity);
        }
    }
}