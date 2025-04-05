using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Game.Scripts.Mechanics
{
    public class ColliderStateChangeFromDeathMechanic : IAtomicLogic
    {
        private readonly IAtomicObservable<bool> _isDead;
        private readonly Collider _collider;

        public ColliderStateChangeFromDeathMechanic(IAtomicObservable<bool> isDead, Collider collider)
        {
            _isDead = isDead;
            _collider = collider;
            
            _isDead.Subscribe(OnDeadStateChanged);
        }

        private void OnDeadStateChanged(bool newState)
        {
            _collider.enabled = !newState;
        }

        ~ColliderStateChangeFromDeathMechanic()
        {
            _isDead.Unsubscribe(OnDeadStateChanged);
        }
    }
}