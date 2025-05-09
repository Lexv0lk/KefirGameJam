using System;
using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Configs.Models;
using Game.Scripts.Fabrics;
using Game.Scripts.Mechanics;
using Game.Scripts.Models;
using UnityEngine;

namespace Game.Scripts.Components
{
    [Serializable]
    public class ShootComponent
    {
        public AtomicVariable<WeaponConfig> CurrentWeapon;
        
        public AtomicEvent ShootRequest;
        public AtomicEvent ShootAction;
        public AtomicEvent ShootEvent;

        public AtomicAnd CanShoot;
        
        [SerializeField] private Transform _shootPoint;

        private IBulletFabric _bulletFabric;
        private RiffleStoreModel _riffleStoreModel;
        private AtomicVariable<float> _reloadTimeLeft;
        private AtomicVariable<float> _reloadTime;
        
        private List<IAtomicLogic> _mechanics = new();   

        public void Compose(IBulletFabric bulletFabric, RiffleStoreModel riffleStoreModel)
        {
            _reloadTimeLeft = new AtomicVariable<float>(0);
            _reloadTime = new AtomicVariable<float>(CurrentWeapon.Value.ReloadTime);
            
            _bulletFabric = bulletFabric;
            _riffleStoreModel = riffleStoreModel;
            
            AtomicFunction<bool> isReloaded = new AtomicFunction<bool>(IsReloaded);
            AtomicFunction<bool> isAmmoEnough = new AtomicFunction<bool>(IsAmmoEnough);
            
            CanShoot.Append(isReloaded);
            CanShoot.Append(isAmmoEnough);
            
            AtomicFunction<Transform> shootPoint = new AtomicFunction<Transform>(GetShootPoint);

            ShootMechanic shootMechanic = new ShootMechanic(CanShoot, _bulletFabric, shootPoint, _reloadTimeLeft,
                ShootRequest, ShootEvent, _reloadTime, _riffleStoreModel, CurrentWeapon);
            ReloadMechanic reloadMechanic = new ReloadMechanic(_reloadTimeLeft);
            
            _mechanics.Add(shootMechanic);
            _mechanics.Add(reloadMechanic);
            
            CurrentWeapon.Subscribe(OnWeaponChanged);
        }

        private void OnWeaponChanged(WeaponConfig newWeapon)
        {
            _reloadTime.Value = newWeapon.ReloadTime;
        }

        public IEnumerable<IAtomicLogic> GetMechanics() => _mechanics;

        private bool IsReloaded()
        {
            return _reloadTimeLeft.Value <= 0;
        }

        private bool IsAmmoEnough()
        {
            return _riffleStoreModel.AmmunitionAmount.Value > 0;
        }

        private Transform GetShootPoint()
        {
            return _shootPoint;
        }

        public void Dispose()
        {
            CurrentWeapon.Unsubscribe(OnWeaponChanged);
        }
    }
}