using System;
using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Mechanics;
using UnityEngine;

namespace Game.Scripts.Components
{
    [Serializable]
    public class AttackComponent
    {
        public AtomicEvent AttackRequest;
        public AtomicEvent AttackAction;
        public AtomicEvent AttackEvent;
        public AtomicEvent AttackEndEvent;

        public AtomicVariable<bool> IsInAttack;
        public AtomicAnd CanAttack;

        public AtomicVariable<float> AttackRange;
        
        [SerializeField] private int _damage;
        [SerializeField] private float _hitReloadTime;

        private IAtomicAction<int> _targetTakeDamageAction;
        private IAtomicValue<bool> _isTargetReached;

        private List<IAtomicLogic> _mechanics = new();

        public void Compose(IAtomicValue<bool> isTargetReached, IAtomicValue<AtomicEntity> target)
        {
            _isTargetReached = isTargetReached;
            CanAttack.Append(_isTargetReached);
            
            ReloadingHitMechanic hitMechanic = new ReloadingHitMechanic(CanAttack,
                AttackRequest, _hitReloadTime);

            ConditionalStateAttackMechanic attackMechanic = new ConditionalStateAttackMechanic(AttackRequest,
                AttackAction, AttackEndEvent, CanAttack, target, AttackEvent, IsInAttack,
                _damage);
            
            _mechanics.Add(hitMechanic);
            _mechanics.Add(attackMechanic);
        }

        public IEnumerable<IAtomicLogic> GetMechanics() => _mechanics;

        public void Reset()
        {
            IsInAttack.Value = false;
        }
    }
}