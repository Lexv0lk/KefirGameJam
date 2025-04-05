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
    public class TargetMoveComponent
    {
        public AtomicVariable<Vector3> Destination;
        public AtomicVariable<bool> IsMoving;
        public AtomicVariable<float> DistanceToDestination;
        public AtomicVariable<float> StoppingDistance;

        public AtomicAnd CanMove;

        [SerializeField] private NavMeshAgent _agent;

        private List<IAtomicLogic> _mechanics = new();

        public void Compose()
        {
            StoppingDistance = new AtomicVariable<float>(_agent.stoppingDistance);
            
            DestinationMoveMechanic simpleMoveMechanic = new DestinationMoveMechanic(Destination, _agent, CanMove);
            IsNavMeshMovingChangeMechanic isMovingChangeMechanic =
                new IsNavMeshMovingChangeMechanic(IsMoving, DistanceToDestination, CanMove, StoppingDistance);
            DistanceToTargetCalculationMechanic distanceToTargetCalculationMechanic =
                new DistanceToTargetCalculationMechanic(DistanceToDestination, Destination, _agent.transform);
            
            _mechanics.Add(simpleMoveMechanic);
            _mechanics.Add(isMovingChangeMechanic);
            _mechanics.Add(distanceToTargetCalculationMechanic);
        }

        public IEnumerable<IAtomicLogic> GetMechanics() => _mechanics;
    }
}