using Atomic.Elements;
using Atomic.Objects;

namespace Game.Scripts.Mechanics
{
    public class ReloadingHitMechanic : IAtomicUpdate
    {
        private readonly IAtomicValue<bool> _isReachedTarget;
        private readonly IAtomicAction _hitAction;
        private readonly float _reloadTime;

        private float _currentReloadTime;

        public ReloadingHitMechanic(IAtomicValue<bool> isReachedTarget, IAtomicAction hitAction,
            float reloadTime)
        {
            _isReachedTarget = isReachedTarget;
            _hitAction = hitAction;
            _reloadTime = reloadTime;
        }
        
        public void OnUpdate(float deltaTime)
        {
            _currentReloadTime -= deltaTime;
            
            if (_isReachedTarget.Value && _currentReloadTime <= 0)
            {
                _hitAction.Invoke();
                _currentReloadTime = _reloadTime;
            }
        }
    }
}