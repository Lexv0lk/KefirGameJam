using System;
using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Tech;
using UnityEngine;

namespace Game.Scripts.Pools
{
    public abstract class Pool<TPrefab> : MonoBehaviour where TPrefab : MonoBehaviour
    {
        [SerializeField] private TPrefab _prefab;
        [SerializeField] private int _startCount;
        [SerializeField] private Transform _root;

        private readonly Queue<TPrefab> _spawnedInstances = new();

        public event Action<TPrefab> Given;
        public event Action<TPrefab> Released;

        public void SetInfo(TPrefab prefab, int startCount, Transform root)
        {
            _prefab = prefab;
            _startCount = startCount;
            _root = root;
        }

        public void Initialize()
        {
            for (int i = 0; i < _startCount - _spawnedInstances.Count; i++)
                AddNewInstance(); 
        }

        private void Awake()
        {
            Initialize();
        }

        private void AddNewInstance()
        {
            TPrefab instance = Instantiate(_prefab, _root);
            instance.gameObject.SetActive(false);
            _spawnedInstances.Enqueue(instance);
        }

        public TPrefab Get()
        {
            if (_spawnedInstances.Count == 0)
                AddNewInstance();

            TPrefab instance = _spawnedInstances.Dequeue();
            instance.gameObject.SetActive(true);
            
            Given?.Invoke(instance);
            
            return instance;
        }

        public void ReleaseEntity(TPrefab instance)
        {
            instance.gameObject.SetActive(false);
            _spawnedInstances.Enqueue(instance);
            Released?.Invoke(instance);
        }
    }
}