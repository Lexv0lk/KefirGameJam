using Atomic.Elements;
using Atomic.Objects;

namespace Game.Scripts.Mechanics
{
    public class TargetReachCalculationMechanic : IAtomicUpdate
    {
        private readonly IAtomicValue<float> _distanceToDestination;
        private readonly IAtomicValue<float> _attackRange;

        private AtomicVariable<bool> _isTargetReached = new(false);

        public IAtomicValueObservable<bool> IsTargetReached => _isTargetReached;

        public TargetReachCalculationMechanic(IAtomicValue<float> distanceToDestination,
            IAtomicValue<float> attackRange)
        {
            _distanceToDestination = distanceToDestination;
            _attackRange = attackRange;
        }

        public void OnUpdate(float deltaTime)
        {
            _isTargetReached.Value = _distanceToDestination.Value <= _attackRange.Value;
        }
    }
}