using Atomic.Elements;
using Atomic.Objects;
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
        private readonly float _reloadTime;
        private readonly RiffleStoreModel _riffleStoreModel;

        public ShootMechanic(IAtomicValue<bool> condition, IBulletFabric bulletFabric, IAtomicValue<Transform> shootPoint, 
            IAtomicVariable<float> reloadTimeLeft, IAtomicObservable shootRequest, IAtomicAction shootEvent, float reloadTime, RiffleStoreModel riffleStoreModel)
        {
            _condition = condition;
            _bulletFabric = bulletFabric;
            _shootPoint = shootPoint;
            _reloadTimeLeft = reloadTimeLeft;
            _shootRequest = shootRequest;
            _shootEvent = shootEvent;
            _reloadTime = reloadTime;
            _riffleStoreModel = riffleStoreModel;
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
            
            AtomicEntity bullet = _bulletFabric.GetBullet();
            
            bullet.transform.position = _shootPoint.Value.position;
            bullet.Get<IAtomicVariable<Vector3>>(MoveAPI.MOVE_DIRECTION).Value = _shootPoint.Value.forward;

            _reloadTimeLeft.Value = _reloadTime;
            _riffleStoreModel.AmmunitionAmount.Value--;
            
            _shootEvent.Invoke();
        }
    }
}