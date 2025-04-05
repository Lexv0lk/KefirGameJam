using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Game.Scripts.Mechanics
{
    public class RigidbodyMoveMechanic : IAtomicEnable, IAtomicDisable
    {
        private readonly IAtomicValueObservable<Vector3> _direction;
        private readonly Rigidbody _rigidbody;
        private readonly float _speed;

        public RigidbodyMoveMechanic(IAtomicValueObservable<Vector3> direction, Rigidbody rigidbody, float speed)
        {
            _direction = direction;
            _rigidbody = rigidbody;
            _speed = speed;
        }

        public void Enable()
        {
            _direction.Subscribe(OnDirectionChanged);
            OnDirectionChanged(_direction.Value);
        }

        public void Disable()
        {
            _direction.Unsubscribe(OnDirectionChanged);
        }
        
        private void OnDirectionChanged(Vector3 newDirection)
        {
            _rigidbody.velocity = _direction.Value.normalized * _speed;
        }
    }
}