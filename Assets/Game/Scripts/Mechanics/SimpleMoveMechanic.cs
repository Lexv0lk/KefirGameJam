using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Game.Scripts.Mechanics
{
    public class SimpleMoveMechanic : IAtomicUpdate
    {
        private readonly IAtomicValue<bool> _canMove;
        private readonly Transform _root;
        private readonly IAtomicValue<Vector3> _direction;
        private readonly IAtomicValue<float> _speed;

        public SimpleMoveMechanic(IAtomicValue<bool> canMove, Transform root,
            IAtomicValue<Vector3> direction, IAtomicValue<float> speed)
        {
            _canMove = canMove;
            _root = root;
            _direction = direction;
            _speed = speed;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_canMove.Value == false)
                return;
            
            _root.position += _direction.Value * (_speed.Value * deltaTime);
        }
    }
}