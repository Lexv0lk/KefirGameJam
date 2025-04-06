using System;
using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Configs;
using Game.Scripts.Configs.Models;
using Game.Scripts.Models;
using Game.Scripts.Tech;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Controllers
{
    public class WeaponChangeController : IInitializable
    {
        private readonly AtomicEntity _player;
        private readonly RiffleStoreModel _riffleStoreModel;
        private readonly WeaponChangeConfig _config;
        
        public WeaponConfig CurrentWeapon => _player.Get<IAtomicVariable<WeaponConfig>>(ShootAPI.CURRENT_WEAPON).Value;

        public event Action<WeaponConfig> Changed; 

        public WeaponChangeController(AtomicEntity player, RiffleStoreModel riffleStoreModel, WeaponChangeConfig config)
        {
            _player = player;
            _riffleStoreModel = riffleStoreModel;
            _config = config;
        }
        
        public void Initialize()
        {
            Change(_config.StartWeapon, _config.StartWeapon.AmmoAmount);
        }

        public void Change(WeaponConfig newWeapon, int ammoAmount)
        {
            ammoAmount = Mathf.Clamp(ammoAmount, 0, newWeapon.AmmoAmount);
            
            _player.Get<IAtomicVariable<WeaponConfig>>(ShootAPI.CURRENT_WEAPON).Value = newWeapon;
            
            _riffleStoreModel.MaxAmmunitionAmount.Value = newWeapon.AmmoAmount;
            _riffleStoreModel.AmmunitionAmount.Value = ammoAmount;
            
            Changed?.Invoke(newWeapon);
        }
    }
}