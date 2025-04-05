using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Game.Scripts.Mechanics.Animation
{
    public class TriggerAnimationMechanics : IAtomicEnable, IAtomicDisable
    {
        private readonly IAtomicObservable _triggerEvent;
        private readonly Animator _animator;
        private readonly int _triggerKey;

        public TriggerAnimationMechanics(IAtomicObservable triggerEvent, Animator animator, int triggerKey)
        {
            _triggerEvent = triggerEvent;
            _animator = animator;
            _triggerKey = triggerKey;
        }

        public void Enable()
        {
            _triggerEvent.Subscribe(OnEventTriggered);
        }

        public void Disable()
        {
            _triggerEvent.Unsubscribe(OnEventTriggered);
        }

        private void OnEventTriggered()
        {
            _animator.SetTrigger(_triggerKey);
        }
    }
}