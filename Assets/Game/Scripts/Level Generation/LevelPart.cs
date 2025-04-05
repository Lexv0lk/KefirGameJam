using System;
using Atomic.Objects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.LevelGeneration
{
    [RequireComponent(typeof(Collider))]
    public class LevelPart : AtomicObject
    {
        [SerializeField, ReadOnly] private int _id;
        [SerializeField] private Transform _leftExit;
        [SerializeField] private Transform _upExit;
        [SerializeField] private Transform _rightExit;
        [SerializeField] private Transform _downExit;
        
        public Transform LeftExit => _leftExit;
        public Transform UpExit => _upExit;
        public Transform RightExit => _rightExit;
        public Transform DownExit => _downExit;
        public int Id => _id;
        
        public event Action<LevelPart> PlayerEntered;
        
        public void SetId(int id)
        {
            _id = id;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            PlayerEntered?.Invoke(this);
        }
    }
}