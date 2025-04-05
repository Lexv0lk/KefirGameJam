using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using UnityEngine;

namespace Game.Scripts.Mechanics.Animation
{
    public class BoolAnimationMechanics : IAtomicEnable, IAtomicDisable
    {
        private readonly IAtomicVariableObservable<bool> _condition;
        private readonly Animator _animator;
        private readonly int _animatorKey;
        private readonly bool _showDebug;

        public BoolAnimationMechanics(IAtomicVariableObservable<bool> condition, Animator animator, int animatorKey)
        {
            _condition = condition;
            _animator = animator;
            _animatorKey = animatorKey;
        }

        public void Enable()
        {
            OnConditionChanged(_condition.Value);
            _condition.Subscribe(OnConditionChanged);
        }

        public void Disable()
        {
            _condition.Unsubscribe(OnConditionChanged);
        }

        private void OnConditionChanged(bool value)
        {
            _animator.SetBool(_animatorKey, value);
        }
    }
}