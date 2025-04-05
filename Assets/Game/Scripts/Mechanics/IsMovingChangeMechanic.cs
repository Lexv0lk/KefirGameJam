using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Scripts.Mechanics
{
    public class IsMovingChangeMechanic : IAtomicUpdate
    {
        private readonly IAtomicVariable<bool> _isMoving;
        private readonly IAtomicValueObservable<Vector3> _direction;
        private readonly IAtomicValue<bool> _canMove;

        public IsMovingChangeMechanic(IAtomicVariable<bool> isMoving, IAtomicValueObservable<Vector3> direction,
            IAtomicValue<bool> canMove)
        {
            _isMoving = isMoving;
            _direction = direction;
            _canMove = canMove;
        }

        public void OnUpdate(float deltaTime)
        {
            _isMoving.Value = _direction.Value != Vector3.zero && _canMove.Value;
        }
    }
}