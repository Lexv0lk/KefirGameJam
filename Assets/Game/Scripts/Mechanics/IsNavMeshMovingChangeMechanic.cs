using Atomic.Elements;
using Atomic.Objects;

namespace Game.Scripts.Mechanics
{
    public class IsNavMeshMovingChangeMechanic : IAtomicUpdate
    {
        private readonly IAtomicVariable<bool> _isMoving;
        private readonly IAtomicValue<float> _distanceToDestination;
        private readonly IAtomicValue<bool> _canMove;
        private readonly IAtomicValue<float> _stoppingDistance;

        public IsNavMeshMovingChangeMechanic(IAtomicVariable<bool> isMoving, IAtomicValue<float> distanceToDestination,
            IAtomicValue<bool> canMove, IAtomicValue<float> stoppingDistance)
        {
            _isMoving = isMoving;
            _distanceToDestination = distanceToDestination;
            _canMove = canMove;
            _stoppingDistance = stoppingDistance;
        }

        public void OnUpdate(float deltaTime)
        {
            _isMoving.Value = _canMove.Value && _distanceToDestination.Value > _stoppingDistance.Value;
        }
    }
}