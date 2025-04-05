using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Game.Scripts.Mechanics
{
    public class DistanceToTargetCalculationMechanic : IAtomicUpdate
    {
        private readonly IAtomicVariable<float> _distanceToDestination;
        private readonly IAtomicValueObservable<Vector3> _destination;
        private readonly Transform _transform;

        public DistanceToTargetCalculationMechanic(IAtomicVariable<float> distanceToDestination,
            IAtomicValueObservable<Vector3> destination, Transform transform)
        {
            _distanceToDestination = distanceToDestination;
            _destination = destination;
            _transform = transform;
        }

        public void OnUpdate(float deltaTime)
        {
            _distanceToDestination.Value = Vector3.Distance(_transform.position, _destination.Value);
        }
    }
}