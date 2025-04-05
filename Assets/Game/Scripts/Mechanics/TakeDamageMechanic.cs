using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Game.Scripts.Mechanics
{
    public class TakeDamageMechanic : IAtomicEnable, IAtomicDisable
    {
        private readonly IAtomicEvent<int> _takeDamageAction;
        private readonly IAtomicVariable<bool> _isDead;
        private readonly IAtomicVariable<int> _healthAmount;
        private readonly IAtomicEvent<int> _takeDamageEvent;

        public TakeDamageMechanic(IAtomicEvent<int> takeDamageAction, IAtomicVariable<bool> isDead,
            IAtomicVariable<int> healthAmount, IAtomicEvent<int> takeDamageEvent)
        {
            _takeDamageAction = takeDamageAction;
            _isDead = isDead;
            _healthAmount = healthAmount;
            _takeDamageEvent = takeDamageEvent;
        }
        
        public void Enable()
        {
            _takeDamageAction.Subscribe(TakeDamage);
        }

        public void Disable()
        {
            _takeDamageAction.Unsubscribe(TakeDamage);
        }
        
        private void TakeDamage(int value)
        {
            if (_isDead.Value)
                return;

            int lastHealthAmount = _healthAmount.Value;
            _healthAmount.Value = Mathf.Max(0, _healthAmount.Value - value);

            if (_healthAmount.Value == 0)
                _isDead.Value = true;
            
            _takeDamageEvent?.Invoke(lastHealthAmount - _healthAmount.Value);
        }
    }
}