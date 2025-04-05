using System;
using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Mechanics.Animation;
using Game.Scripts.Tech;
using Game.Scripts.Utilities;
using UnityEngine;

namespace Game.Scripts.Entities
{
    [Serializable]
    public class EnemyAnimation
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimatorDispatcher _animatorDispatcher;
        [SerializeField] private EnemyAnimatorKeys _animatorKeys;
        [SerializeField] private EnemyAnimationEvents _animationEvents;
        
        private Action _dieAnimationAction;
        private EnemyCore _core;
        private IAtomicValue<AtomicEntity> _attackTarget;
        
        private AttackAnimationMechanics _attackAnimationMechanics;
        private BoolAnimationMechanics _moveAnimationMechanics;
        private BoolAnimationMechanics _deadAnimationMechanics;
        
        private List<IAtomicLogic> _mechanics = new();

        public void Compose(EnemyCore core, IAtomicValue<AtomicEntity> attackTarget, Action dieAnimationAction)
        {
            _core = core;
            _attackTarget = attackTarget;
            _dieAnimationAction = dieAnimationAction;
            
            AtomicFunction<bool> canAttack = new AtomicFunction<bool>(CanAttack);
            
            _moveAnimationMechanics = new BoolAnimationMechanics(_core.MoveComponent.IsMoving,
                _animator, Animator.StringToHash(_animatorKeys.MoveBoolean));

            _deadAnimationMechanics = new BoolAnimationMechanics(_core.LifeComponent.IsDead, _animator,
                Animator.StringToHash(_animatorKeys.DeadBoolean));
            
            _attackAnimationMechanics = new AttackAnimationMechanics(_animator,
                _animatorDispatcher, _animatorKeys.AttackTrigger, _animationEvents.AttackEvent,
                core.AttackComponent.AttackRequest, core.AttackComponent.AttackAction,
                canAttack);
            
            _animatorDispatcher.SubscribeOnEvent(_animationEvents.DeadEvent, _dieAnimationAction);
            _animatorDispatcher.SubscribeOnEvent(_animationEvents.AttackEndEvent, OnAttackAnimationEnded);
            
            _mechanics.Add(_attackAnimationMechanics);
            _mechanics.Add(_moveAnimationMechanics);
            _mechanics.Add(_deadAnimationMechanics);
        }

        public void Dispose()
        {
            _animatorDispatcher.UnsubscribeOnEvent(_animationEvents.AttackEndEvent, OnAttackAnimationEnded);
            _animatorDispatcher.UnsubscribeOnEvent(_animationEvents.DeadEvent, _dieAnimationAction);
        }

        public IEnumerable<IAtomicLogic> GetMechanics() => _mechanics;

        private void OnAttackAnimationEnded()
        {
            _core.AttackComponent.AttackEndEvent.Invoke();
        }

        private bool CanAttack()
        {
            return _attackTarget.Value && _attackTarget.Value.Get<IAtomicValue<bool>>(LifeAPI.IS_DEAD).Value == false;
        }
    }
}