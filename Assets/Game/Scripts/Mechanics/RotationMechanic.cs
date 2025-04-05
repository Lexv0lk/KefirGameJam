using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Game.Scripts.Mechanics
{
    public class RotationMechanic : IAtomicUpdate
    {
        private readonly Transform _root;
        private readonly IAtomicValue<bool> _canRotate;
        private readonly IAtomicValue<Vector3> _forwardDirection;
        private readonly float _speed;
        
        private Quaternion _cachedTargetRotation;

        public RotationMechanic(IAtomicValue<bool> canRotate, Transform root,
            IAtomicValue<Vector3> forwardDirection, float speed)
        {
            _canRotate = canRotate;
            _root = root;
            _forwardDirection = forwardDirection;
            _speed = speed;
        }
        
        public void OnUpdate(float deltaTime)
        {
            if (_canRotate.Value == false)
                return;
            
            if (_root.forward == _forwardDirection.Value)
                return;

            _cachedTargetRotation = Quaternion.LookRotation(_forwardDirection.Value.normalized, Vector3.up);
            _root.rotation = Quaternion.Lerp(_root.rotation, _cachedTargetRotation, _speed * Time.deltaTime);
        }
    }
}