using System;
using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Mechanics;
using UnityEngine;

namespace Game.Scripts.Components
{
    [Serializable]
    public class RigidbodyMoveComponent
    {
        public AtomicVariable<Vector3> Direction;

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _speed;
        
        private List<IAtomicLogic> _mechanics = new();

        public void Compose()
        {
            RigidbodyMoveMechanic moveMechanic = new RigidbodyMoveMechanic(Direction, _rigidbody, _speed);
            _mechanics.Add(moveMechanic);
        }
        
        public IEnumerable<IAtomicLogic> GetMechanics() => _mechanics;
    }
}