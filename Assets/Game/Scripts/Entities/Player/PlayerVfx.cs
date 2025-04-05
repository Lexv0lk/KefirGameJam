using System;
using UnityEngine;

namespace Game.Scripts.Entities
{
    [Serializable]
    public class PlayerVfx
    {
        [SerializeField] private ParticleSystem _shootVfx;
        [SerializeField] private ParticleSystem _takeDamageVfx;

        private PlayerCore _playerCore;
        
        public void Compose(PlayerCore core)
        {
            _playerCore = core;
            _playerCore.ShootComponent.ShootEvent.Subscribe(PlayShootVfx);
            _playerCore.LifeComponent.TakeDamageEvent.Subscribe(PlayTakeDamageVfx);
        }

        public void Dispose()
        {
            _playerCore.ShootComponent.ShootEvent.Unsubscribe(PlayShootVfx);
            _playerCore.LifeComponent.TakeDamageEvent.Unsubscribe(PlayTakeDamageVfx);
        }

        private void PlayShootVfx()
        {
            _shootVfx.Play();
        }
        
        private void PlayTakeDamageVfx(int _)
        {
            _takeDamageVfx.Play();
        }
    }
}