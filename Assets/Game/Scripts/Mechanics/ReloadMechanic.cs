using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Game.Scripts.Mechanics
{
    public class ReloadMechanic : IAtomicUpdate
    {
        private readonly IAtomicVariable<float> _reloadTimeLeft;

        public ReloadMechanic(IAtomicVariable<float> reloadTimeLeft)
        {
            _reloadTimeLeft = reloadTimeLeft;
        }
        
        public void OnUpdate(float deltaTime)
        {
            _reloadTimeLeft.Value = Mathf.Max(0, _reloadTimeLeft.Value - deltaTime);
        }
    }
}