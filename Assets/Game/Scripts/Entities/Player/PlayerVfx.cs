using System;
using Game.Audio;
using Game.Scripts.Configs.Models;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Entities
{
    [Serializable]
    public class PlayerVfx
    {
        [SerializeField] private Transform _weaponRoot;
        [SerializeField] private ParticleSystem _shootVfx;
        [SerializeField] private ParticleSystem _takeDamageVfx;
        [SerializeField] private AudioClip _takeDamageSound;
        [SerializeField] private AudioClip _weaponChangeSound;

        private PlayerCore _playerCore;
        private AudioClip _shootAudio;
        private AudioController _audioController;
        private Transform _root;
        private GameObject _lastWeapon;

        public void Compose(PlayerCore core, Transform root, AudioController audioController)
        {
            _root = root;
            _playerCore = core;
            _audioController = audioController;
            _playerCore.ShootComponent.ShootEvent.Subscribe(PlayShootVfx);
            _playerCore.LifeComponent.TakeDamageEvent.Subscribe(PlayTakeDamageVfx);
            _playerCore.ShootComponent.CurrentWeapon.Subscribe(PlayWeaponChangeVfx);
        }

        public void Dispose()
        {
            _playerCore.ShootComponent.ShootEvent.Unsubscribe(PlayShootVfx);
            _playerCore.LifeComponent.TakeDamageEvent.Unsubscribe(PlayTakeDamageVfx);
            _playerCore.ShootComponent.CurrentWeapon.Unsubscribe(PlayWeaponChangeVfx);
        }

        private void PlayShootVfx()
        {
            _shootVfx.Play();
            var audioClip = _playerCore.ShootComponent.CurrentWeapon.Value.Metadata.ShotSound;
            _audioController.PlaySound(audioClip, _root.position, Random.Range(0.95f, 1.05f));
        }
        
        private void PlayTakeDamageVfx(int _)
        {
            _takeDamageVfx.Play();
            _audioController.PlaySound(_takeDamageSound, _root.position, Random.Range(0.95f, 1.05f));
        }

        private void PlayWeaponChangeVfx(WeaponConfig weaponConfig)
        {
            if (_lastWeapon != null)
                GameObject.Destroy(_lastWeapon);

            _lastWeapon = GameObject.Instantiate(weaponConfig.Metadata.Prefab, _weaponRoot);
            _audioController.PlaySound(_weaponChangeSound, _root.position, Random.Range(0.95f, 1.05f));
        }
    }
}