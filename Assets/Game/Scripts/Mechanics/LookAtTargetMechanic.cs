using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Game.Scripts.Mechanics
{
    public class LookAtTargetMechanic : IAtomicUpdate
    {
        private readonly IAtomicValue<AtomicEntity> _target;
        private readonly IAtomicValue<Vector3> _rootPosition;
        private readonly IAtomicVariable<Vector3> _rootForwardDirection;

        public LookAtTargetMechanic(IAtomicValue<AtomicEntity> target, IAtomicValue<Vector3> rootPosition,
            IAtomicVariable<Vector3> rootForwardDirection)
        {
            _target = target;
            _rootPosition = rootPosition;
            _rootForwardDirection = rootForwardDirection;
        }
        
        public void OnUpdate(float deltaTime)
        {
            if (_target.Value is null)
                return;
            
            _rootForwardDirection.Value = _target.Value.transform.position - _rootPosition.Value;
        }
    }
}