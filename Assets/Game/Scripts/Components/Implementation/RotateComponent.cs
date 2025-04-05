using System;
using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Mechanics;
using UnityEngine;

namespace Game.Scripts.Components
{
    [Serializable]
    public class RotateComponent
    {
        public AtomicVariable<Vector3> ForwardDirection;

        public AtomicAnd CanRotate;

        [SerializeField] private Transform _root;
        [SerializeField] private float _speed;
        
        private List<IAtomicLogic> _mechanics = new();

        public void Compose()
        {
            ForwardDirection.Value = _root.forward;
            
            RotationMechanic rotationMechanic = new RotationMechanic(CanRotate, _root, ForwardDirection, _speed);
            
            _mechanics.Add(rotationMechanic);
        }

        public IEnumerable<IAtomicLogic> GetMechanics() => _mechanics;
    }
}