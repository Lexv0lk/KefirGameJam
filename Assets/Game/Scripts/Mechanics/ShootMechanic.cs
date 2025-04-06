using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Configs.Models;
using Game.Scripts.Fabrics;
using Game.Scripts.Models;
using Game.Scripts.Tech;
using UnityEngine;

namespace Game.Scripts.Mechanics
{
    public class ShootMechanic : IAtomicEnable, IAtomicDisable
    {
        private readonly IAtomicValue<bool> _condition;
        private readonly IBulletFabric _bulletFabric;
        private readonly IAtomicValue<Transform> _shootPoint;
        private readonly IAtomicVariable<float> _reloadTimeLeft;
        private readonly IAtomicObservable _shootRequest;
        private readonly IAtomicAction _shootEvent;
        private readonly IAtomicValueObservable<float> _reloadTime;
        private readonly RiffleStoreModel _riffleStoreModel;
        private readonly IAtomicValue<WeaponConfig> _currentWeapon;

        public ShootMechanic(IAtomicValue<bool> condition, IBulletFabric bulletFabric,
            IAtomicValue<Transform> shootPoint, IAtomicVariable<float> reloadTimeLeft, IAtomicObservable shootRequest,
            IAtomicAction shootEvent, IAtomicValueObservable<float> reloadTime, RiffleStoreModel riffleStoreModel, 
            IAtomicValue<WeaponConfig> currentWeapon)
        {
            _condition = condition;
            _bulletFabric = bulletFabric;
            _shootPoint = shootPoint;
            _reloadTimeLeft = reloadTimeLeft;
            _shootRequest = shootRequest;
            _shootEvent = shootEvent;
            _reloadTime = reloadTime;
            _riffleStoreModel = riffleStoreModel;
            _currentWeapon = currentWeapon;
        }

        public void Enable()
        {
            _shootRequest.Subscribe(Shoot);
        }

        public void Disable()
        {
            _shootRequest.Unsubscribe(Shoot);
        }
        
        private void Shoot()
        {
            if (_condition.Value == false)
                return;

            if (_currentWeapon.Value is ShotgunWeaponConfig shotgunWeaponConfig)
            {
                ProcessMultipleShot(shotgunWeaponConfig);
            }
            else
            {
                ProcessSimpleShot(_currentWeapon.Value);
            }

            _reloadTimeLeft.Value = _reloadTime.Value;
            _riffleStoreModel.AmmunitionAmount.Value--;
            
            _shootEvent.Invoke();
        }

        private void ProcessSimpleShot(WeaponConfig weaponConfig)
        {
            AtomicEntity bullet = _bulletFabric.GetBullet(weaponConfig.BulletPrefab);
            bullet.Get<IAtomicVariable<int>>(ShootAPI.DAMAGE).Value = weaponConfig.Damage;
            bullet.transform.position = _shootPoint.Value.position;

            var forwardXZ = new Vector3(_shootPoint.Value.forward.x, 0, _shootPoint.Value.forward.z);
            
            bullet.Get<IAtomicVariable<Vector3>>(MoveAPI.MOVE_DIRECTION).Value = forwardXZ;
        }

        private void ProcessMultipleShot(ShotgunWeaponConfig weaponConfig)
        {
            float totalAngle = weaponConfig.ShootAngle;
            float angle = totalAngle / (weaponConfig.ShotBulletsCount - 1);
            
            for (float i = - totalAngle / 2; i <= totalAngle / 2; i += angle)
            {
                AtomicEntity bullet = _bulletFabric.GetBullet(weaponConfig.BulletPrefab);
                bullet.Get<IAtomicVariable<int>>(ShootAPI.DAMAGE).Value = weaponConfig.Damage;
                bullet.transform.position = _shootPoint.Value.position;
                Quaternion rotation = Quaternion.Euler(0, i, 0);
                bullet.Get<IAtomicVariable<Vector3>>(MoveAPI.MOVE_DIRECTION).Value = rotation * _shootPoint.Value.forward;
            }
        }
    }
}