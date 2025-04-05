using System;
using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Tech;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Pools
{
    public abstract class AtomicEntityPool : MonoBehaviour, IAtomicEntityPool
    {
        [SerializeField] private AtomicEntity _prefab;
        [SerializeField] private int _startCount;
        [SerializeField] private Transform _root;

        private readonly Queue<AtomicEntity> _spawnedEntities = new();

        public event Action<AtomicEntity> Given;
        public event Action<AtomicEntity> Released;

        public void SetInfo(AtomicEntity prefab, int startCount, Transform root)
        {
            _prefab = prefab;
            _startCount = startCount;
            _root = root;
        }

        public void Initialize()
        {
            for (int i = 0; i < _startCount - _spawnedEntities.Count; i++)
                AddNewEntity(); 
        }

        private void Awake()
        {
            Initialize();
        }

        private void AddNewEntity()
        {
            AtomicEntity entity = Instantiate(_prefab, _root);
            entity.gameObject.SetActive(false);
            _spawnedEntities.Enqueue(entity);
        }

        public AtomicEntity GetEntity()
        {
            if (_spawnedEntities.Count == 0)
                AddNewEntity();

            AtomicEntity entity = _spawnedEntities.Dequeue();
            entity.gameObject.SetActive(true);

            if (entity.TryGet<IAtomicAction>(TechAPI.RESET_ACTION, out var resetAction))
                resetAction.Invoke();
            
            Given?.Invoke(entity);
            
            return entity;
        }

        public void ReleaseEntity(AtomicEntity entity)
        {
            entity.gameObject.SetActive(false);
            _spawnedEntities.Enqueue(entity);
            Released?.Invoke(entity);
        }

        protected abstract AtomicEntity Instantiate(AtomicEntity prefab, Transform root);
    }
}