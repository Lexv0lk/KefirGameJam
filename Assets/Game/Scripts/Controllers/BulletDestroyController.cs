using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Fabrics;
using Game.Scripts.Tech;
using UnityEngine;

namespace Game.Scripts.Controllers
{
    public class BulletDestroyController
    {
        private readonly CreatingBulletFabric _bulletFabric;

        public BulletDestroyController(CreatingBulletFabric bulletFabric)
        {
            _bulletFabric = bulletFabric;
            _bulletFabric.CreatedBullet += OnBulletCreated;
        }

        private void OnBulletCreated(AtomicEntity bullet)
        {
            bullet.Get<IAtomicEvent<AtomicEntity>>(PhysicsAPI.COLLIDE_EVENT).Subscribe(OnBulletCollided);
        }

        private void OnBulletCollided(AtomicEntity bullet)
        {
            bullet.Get<IAtomicEvent<AtomicEntity>>(PhysicsAPI.COLLIDE_EVENT).Unsubscribe(OnBulletCollided);
            GameObject.Destroy(bullet.gameObject);
        }

        ~BulletDestroyController()
        {
            _bulletFabric.CreatedBullet -= OnBulletCreated;
        }
    }
}