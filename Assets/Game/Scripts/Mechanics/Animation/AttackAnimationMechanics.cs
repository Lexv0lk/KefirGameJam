using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Utilities;
using UnityEngine;

namespace Game.Scripts.Mechanics.Animation
{
    public class AttackAnimationMechanics : IAtomicEnable, IAtomicDisable
    {
        private readonly Animator _animator;
        private readonly AnimatorDispatcher _dispatcher;
        private readonly string _attackTriggerName;
        private readonly string _attackEventName;
        private readonly IAtomicObservable _attackRequest;
        private readonly IAtomicAction _attackAction;
        private readonly IAtomicValue<bool> _canAttack;

        private bool _isInAttackAnimation;

        public AttackAnimationMechanics(Animator animator, AnimatorDispatcher dispatcher, string attackTriggerName,
            string attackEventName, IAtomicObservable attackRequest, IAtomicAction attackAction,
            IAtomicValue<bool> canAttack)
        {
            _animator = animator;
            _dispatcher = dispatcher;
            _attackTriggerName = attackTriggerName;
            _attackEventName = attackEventName;
            _attackRequest = attackRequest;
            _attackAction = attackAction;
            _canAttack = canAttack;
        }
        
        public void Enable()
        {
            _attackRequest.Subscribe(OnAttackRequested);
            _dispatcher.SubscribeOnEvent(_attackEventName, InvokeAttackAction);
            _isInAttackAnimation = false;
        }

        public void Disable()
        {
            _attackRequest.Unsubscribe(OnAttackRequested);
            _dispatcher.UnsubscribeOnEvent(_attackEventName, InvokeAttackAction);
            _isInAttackAnimation = false;
        }
        
        private void OnAttackRequested()
        {
            if (_canAttack.Value && _isInAttackAnimation == false)
            {
                _animator.SetTrigger(_attackTriggerName);
                _isInAttackAnimation = true;
            }
        }

        private void InvokeAttackAction()
        {
            _attackAction.Invoke();
            _isInAttackAnimation = false;
        }
    }
}