using System;
using Game.Audio;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Entities
{
    [Serializable]
    public class EnemyVfx
    {
        [SerializeField] private ParticleSystem _takeDamageVfx;
        [SerializeField] private AudioClip _takeDamageClip;

        private EnemyCore _playerCore;
        private AudioController _audioController;
        private Transform _root;

        public void Compose(EnemyCore core, Transform root, AudioController audioController)
        {
            _playerCore = core;
            _root = root;
            _audioController = audioController;
            _playerCore.LifeComponent.TakeDamageEvent.Subscribe(PlayTakeDamageVfx);
        }

        public void Dispose()
        {
            _playerCore.LifeComponent.TakeDamageEvent.Unsubscribe(PlayTakeDamageVfx);
        }
        
        private void PlayTakeDamageVfx(int _)
        {
            _takeDamageVfx.Play();
            _audioController.PlaySound(_takeDamageClip, _root.position, Random.Range(0.95f, 1.05f));
        }
    }
}