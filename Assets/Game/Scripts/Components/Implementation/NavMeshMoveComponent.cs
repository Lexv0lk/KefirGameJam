using System;
using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Mechanics;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Scripts.Components
{
    [Serializable]
    public class NavMeshMoveComponent
    {
        public AtomicVariable<Vector3> Direction;
        public AtomicVariable<bool> IsMoving;

        public AtomicAnd CanMove;

        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _speed;

        private List<IAtomicLogic> _mechanics = new();

        public void Compose()
        {
            AtomicFunction<float> speed = new AtomicFunction<float>(GetSpeed);

            NavMeshMoveMechanic simpleMoveMechanic = new NavMeshMoveMechanic(CanMove, _agent, Direction, speed);
            IsMovingChangeMechanic isMovingChangeMechanic = new IsMovingChangeMechanic(IsMoving, Direction, CanMove);
            
            _mechanics.Add(simpleMoveMechanic);
            _mechanics.Add(isMovingChangeMechanic);
        }

        public IEnumerable<IAtomicLogic> GetMechanics() => _mechanics;

        private float GetSpeed() => _speed;
    }
}