using System;
using Game.Scripts.Configs.Controllers;
using Game.Scripts.Models;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Controllers
{
    public class WavesController : ITickable
    {
        private readonly WavesConfig _config;
        private readonly WaveData _data;

        private float _currentTime;
        
        public event Action NewWaveStarted;

        public WavesController(WavesConfig config, WaveData data)
        {
            _config = config;
            _data = data;

            _data.CurrentDamage = _config.StartDamage;
            _data.CurrentHealth = _config.StartHealth;
        }
        
        public void Tick()
        {
            _currentTime += Time.deltaTime;

            if (_currentTime >= _config.WaveDuration)
            {
                _currentTime = 0;
                _data.CurrentDamage = Mathf.RoundToInt(_data.CurrentDamage * _config.DamageMultiplier);
                _data.CurrentHealth = Mathf.RoundToInt(_data.CurrentHealth * _config.HealthMultiplier);
                
                NewWaveStarted?.Invoke();
            }
        }
    }
}