using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Tech;

namespace Game.Scripts.Mechanics
{
    public class ConditionalStateAttackMechanic : IAtomicEnable, IAtomicDisable
    {
        private readonly IAtomicObservable _attackRequest;
        private readonly IAtomicObservable _attackAction;
        private readonly IAtomicObservable _attackEndEvent;
        private readonly IAtomicValue<bool> _canAttackTarget;
        private readonly IAtomicValue<AtomicEntity> _target;
        private readonly IAtomicAction _attackEvent;
        private readonly IAtomicVariable<bool> _isInAttack;
        private readonly IAtomicValue<int> _damage;

        public ConditionalStateAttackMechanic(IAtomicObservable attackRequest, IAtomicObservable attackAction,
            IAtomicObservable attackEndEvent, IAtomicValue<bool> canAttackTarget,
            IAtomicValue<AtomicEntity> target, IAtomicAction attackEvent,
            IAtomicVariable<bool> isInAttack, IAtomicValue<int> damage)
        {
            _attackRequest = attackRequest;
            _attackAction = attackAction;
            _attackEndEvent = attackEndEvent;
            _canAttackTarget = canAttackTarget;
            _target = target;
            _attackEvent = attackEvent;
            _isInAttack = isInAttack;
            _damage = damage;
        }

        public void Enable()
        {
            _attackAction.Subscribe(AttackTarget);
            _attackRequest.Subscribe(ChangeToAttackState);
            _attackEndEvent.Subscribe(ChangeToNonAttackState);
        }

        public void Disable()
        {
            _attackAction.Unsubscribe(AttackTarget);
            _attackRequest.Unsubscribe(ChangeToAttackState);
            _attackEndEvent.Unsubscribe(ChangeToNonAttackState);
        }
        
        private void AttackTarget()
        {
            if (_canAttackTarget.Value && _target.Value)
                _target.Value.Get<IAtomicAction<int>>(LifeAPI.TAKE_DAMAGE_ACTION).Invoke(_damage.Value);

            _attackEvent.Invoke();
        }

        private void ChangeToAttackState()
        {
            _isInAttack.Value = true;
        }

        private void ChangeToNonAttackState()
        {
            _isInAttack.Value = false;
        }
    }
}