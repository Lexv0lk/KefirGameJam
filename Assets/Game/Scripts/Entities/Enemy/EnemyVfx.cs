using System;
using UnityEngine;

namespace Game.Scripts.Entities
{
    [Serializable]
    public class EnemyVfx
    {
        [SerializeField] private ParticleSystem _takeDamageVfx;

        private EnemyCore _playerCore;
        
        public void Compose(EnemyCore core)
        {
            _playerCore = core;
            _playerCore.LifeComponent.TakeDamageEvent.Subscribe(PlayTakeDamageVfx);
        }

        public void Dispose()
        {
            _playerCore.LifeComponent.TakeDamageEvent.Unsubscribe(PlayTakeDamageVfx);
        }
        
        private void PlayTakeDamageVfx(int _)
        {
            _takeDamageVfx.Play();
        }
    }
}