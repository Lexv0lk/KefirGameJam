using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Game.Scripts.Mechanics
{
    public class FollowTargetMechanic : IAtomicUpdate
    {
        public IAtomicValue<bool> IsReachedTarget => _isReachedTarget;

        private readonly IAtomicValue<AtomicEntity> _target;
        private readonly IAtomicValue<Vector3> _rootPosition;
        private readonly IAtomicVariable<Vector3> _rootMoveDirection;
        private readonly float _reachDistance;

        private readonly AtomicVariable<bool> _isReachedTarget;

        public FollowTargetMechanic(IAtomicValue<AtomicEntity> target, IAtomicValue<Vector3> rootPosition,
            IAtomicVariable<Vector3> rootMoveDirection, float reachDistance)
        {
            _target = target;
            _rootPosition = rootPosition;
            _rootMoveDirection = rootMoveDirection;
            _reachDistance = reachDistance;

            _isReachedTarget = new AtomicVariable<bool>(false);
        }
        
        public void OnUpdate(float deltaTime)
        {
            if (_target.Value is null)
            {
                _isReachedTarget.Value = false;
                return;
            }

            float currentDistance = Vector3.Distance(_target.Value.transform.position, _rootPosition.Value);
            bool targetReached = currentDistance <= _reachDistance;
            _isReachedTarget.Value = targetReached;

            if (targetReached == false)
            {
                Vector3 direction = _target.Value.transform.position - _rootPosition.Value;
                _rootMoveDirection.Value = direction.normalized;
            }
            else
            {
                _rootMoveDirection.Value = Vector3.zero;
            }
        }
    }
}