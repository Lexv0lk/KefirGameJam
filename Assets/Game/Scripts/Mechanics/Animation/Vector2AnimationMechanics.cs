using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Game.Scripts.Mechanics.Animation
{
    public class Vector2AnimationMechanics : IAtomicUpdate
    {
        private readonly IAtomicValue<Vector2> _source;
        private readonly Animator _animator;
        private readonly int _xAnimatorKey;
        private readonly int _yAnimatorKey;
        private readonly float _changeSpeed;

        private float _currentXValue;
        private float _currentYValue;

        public Vector2AnimationMechanics(IAtomicValue<Vector2> source, Animator animator, int xAnimatorKey,
            int yAnimatorKey, float changeSpeed)
        {
            _source = source;
            _animator = animator;
            _xAnimatorKey = xAnimatorKey;
            _yAnimatorKey = yAnimatorKey;
            _changeSpeed = changeSpeed;
        }
        
        public void OnUpdate(float deltaTime)
        {
            _currentXValue = Mathf.MoveTowards(_currentXValue, _source.Value.x, _changeSpeed * Time.deltaTime);
            _currentYValue = Mathf.MoveTowards(_currentYValue, _source.Value.y, _changeSpeed * Time.deltaTime);
            
            _animator.SetFloat(_xAnimatorKey, _currentXValue);
            _animator.SetFloat(_yAnimatorKey, _currentYValue);
        }

        private void OnSourceValueChanged(Vector2 newValue)
        {
            _animator.SetFloat(_xAnimatorKey, newValue.x);
            _animator.SetFloat(_yAnimatorKey, newValue.y);
        }
    }
}