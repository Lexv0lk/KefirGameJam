using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Game.Scripts.Mechanics.Animation
{
    public class InverseMoveDirectionMechanics : IAtomicUpdate
    {
        public AtomicVariable<Vector2> ConvertedDirection;
        
        private readonly Transform _localTransform;
        private readonly IAtomicValue<Vector3> _originalMoveDirection;

        private Vector3 _cachedNormalizedDirection;
        private Vector3 _cachedTransformedDirection;

        public InverseMoveDirectionMechanics(Transform localTransform, IAtomicValue<Vector3> originalMoveDirection)
        {
            _localTransform = localTransform;
            _originalMoveDirection = originalMoveDirection;

            ConvertedDirection = new AtomicVariable<Vector2>(ConvertDirection(_originalMoveDirection.Value));
        }
        
        public void OnUpdate(float deltaTime)
        {
            ConvertedDirection.Value = ConvertDirection(_originalMoveDirection.Value);
        }

        private Vector2 ConvertDirection(Vector3 origDir)
        {
            _cachedNormalizedDirection = origDir.normalized;
            _cachedTransformedDirection = _localTransform.InverseTransformDirection(_cachedNormalizedDirection);
            return new Vector2(_cachedTransformedDirection.x, _cachedTransformedDirection.z);
        }
    }
}