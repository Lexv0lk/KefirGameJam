using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Game.Scripts.Mechanics
{
    public class FollowTargetMechanic : IAtomicUpdate, IAtomicEnable, IAtomicDisable
    {
        private readonly IAtomicVariable<Vector3> _destination;
        private readonly IAtomicValueObservable<AtomicEntity> _target;
        private readonly IAtomicValue<float> _attackRange;
        private readonly IAtomicVariable<float> _stoppingDistance;
        
        private AtomicVariable<bool> _isTargetReached = new AtomicVariable<bool>(false);

        private Transform _targetTransform;
        
        public FollowTargetMechanic(IAtomicVariable<Vector3> destination, IAtomicValueObservable<AtomicEntity> target,
            IAtomicValue<float> attackRange, IAtomicVariable<float> stoppingDistance)
        {
            _destination = destination;
            _target = target;
            _attackRange = attackRange;
            _stoppingDistance = stoppingDistance;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_targetTransform != null)
                _destination.Value = _targetTransform.position;
        }

        public void Enable()
        {
            _target.Subscribe(OnTargetChanged);
            OnTargetChanged(_target.Value);
        }

        public void Disable()
        {
            _target.Unsubscribe(OnTargetChanged);
        }

        private void OnTargetChanged(AtomicEntity newTarget)
        {
            if (newTarget == null)
            {
                _targetTransform = null;
            }
            else
            {
                _targetTransform = newTarget.transform;
                _stoppingDistance.Value = _attackRange.Value;
            }
        }
    }
}