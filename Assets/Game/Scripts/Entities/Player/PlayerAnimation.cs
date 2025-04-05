using System;
using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Mechanics.Animation;
using Game.Scripts.Utilities;
using UnityEngine;

namespace Game.Scripts.Entities
{
    [Serializable]
    public class PlayerAnimation
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimatorDispatcher _animatorDispatcher;
        [SerializeField] private float _animationValueChangeSpeed = 10;
        [SerializeField] private PlayerAnimatorKeys _animatorKeys;
        [SerializeField] private PlayerAnimationEvents _animationEvents;

        private AtomicEvent _takeDamageEvent = new();
        private PlayerCore _core;
        private Action _dieAnimationAction;
        
        private BoolAnimationMechanics _moveAnimationMechanics;
        private BoolAnimationMechanics _deadAnimationMechanics;
        private Vector2AnimationMechanics _vector2AnimationMechanics;
        private InverseMoveDirectionMechanics _inverseMoveDirectionMechanics;
        private AttackAnimationMechanics _attackAnimationMechanics;
        private TriggerAnimationMechanics _takeDamageAnimationMechanics;

        public void Compose(PlayerCore core, Action dieAnimationAction, Transform transform)
        {
            _core = core;
            _dieAnimationAction = dieAnimationAction;
            
            _moveAnimationMechanics = new BoolAnimationMechanics(core.MoveComponent.IsMoving,
                _animator, Animator.StringToHash(_animatorKeys.MoveBoolean));

            _deadAnimationMechanics = new BoolAnimationMechanics(core.LifeComponent.IsDead, _animator,
                Animator.StringToHash(_animatorKeys.DeadBoolean));
            
            _inverseMoveDirectionMechanics =
                new InverseMoveDirectionMechanics(transform, _core.MoveComponent.Direction);
            
            _vector2AnimationMechanics = new Vector2AnimationMechanics(_inverseMoveDirectionMechanics.ConvertedDirection,
                _animator, Animator.StringToHash(_animatorKeys.MoveXFloat), Animator.StringToHash(_animatorKeys.MoveYFloat), _animationValueChangeSpeed);

            _attackAnimationMechanics = new AttackAnimationMechanics(_animator,
                _animatorDispatcher, _animatorKeys.AttackTrigger, _animationEvents.AttackEvent,
                core.ShootComponent.ShootRequest, core.ShootComponent.ShootAction,
                core.ShootComponent.CanShoot);

            _takeDamageAnimationMechanics = new TriggerAnimationMechanics(_takeDamageEvent,
                _animator, Animator.StringToHash(_animatorKeys.TakeDamageTrigger));
            
            _core.LifeComponent.TakeDamageEvent.Subscribe(OnCharacterTakeDamageEvent);
            _animatorDispatcher.SubscribeOnEvent(_animationEvents.DeadEvent, _dieAnimationAction);
        }

        public void Dispose()
        {
            _core.LifeComponent.TakeDamageEvent.Unsubscribe(OnCharacterTakeDamageEvent);
            _animatorDispatcher.UnsubscribeOnEvent(_animationEvents.DeadEvent, _dieAnimationAction);
        }

        private void OnCharacterTakeDamageEvent(int _)
        {
            _takeDamageEvent.Invoke();
        }

        public IEnumerable<IAtomicLogic> GetMechanics()
        {
            return new IAtomicLogic[] { _vector2AnimationMechanics, _inverseMoveDirectionMechanics,
                _attackAnimationMechanics, _takeDamageAnimationMechanics, _moveAnimationMechanics,
                _deadAnimationMechanics };
        }
    }
}