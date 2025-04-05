using System;
using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Mechanics;

namespace Game.Scripts.Components
{
    [Serializable]
    public class LifeComponent
    {
        public AtomicEvent<int> TakeDamageAction;
        public AtomicEvent<int> TakeDamageEvent;
        
        public AtomicVariable<bool> IsDead;
        public AtomicVariable<int> HealthAmount;

        private int _startHealthAmount;
        private List<IAtomicLogic> _mechanics = new();
        
        public void Compose()
        {
            _startHealthAmount = HealthAmount.Value;
            TakeDamageMechanic takeDamageMechanic =
                new TakeDamageMechanic(TakeDamageAction, IsDead, HealthAmount, TakeDamageEvent);
            _mechanics.Add(takeDamageMechanic);
        }
        
        public IEnumerable<IAtomicLogic> GetMechanics() => _mechanics;

        public void Reset()
        {
            HealthAmount.Value = _startHealthAmount;
            IsDead.Value = false;
        }
    }
}