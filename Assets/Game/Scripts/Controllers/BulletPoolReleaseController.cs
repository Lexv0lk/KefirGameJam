using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Pools;
using Game.Scripts.Tech;

namespace Game.Scripts.Controllers
{
    public class BulletPoolReleaseController
    {
        private readonly IAtomicEntityPool _bulletPool;

        public BulletPoolReleaseController(GamePools gamePools)
        {
            _bulletPool = gamePools.BulletPool;
            _bulletPool.Given += OnBulletGiven;
        }

        private void OnBulletGiven(AtomicEntity bullet)
        {
            bullet.Get<IAtomicEvent<AtomicEntity>>(PhysicsAPI.COLLIDE_EVENT).Subscribe(OnBulletCollided);
        }

        private void OnBulletCollided(AtomicEntity bullet)
        {
            bullet.Get<IAtomicEvent<AtomicEntity>>(PhysicsAPI.COLLIDE_EVENT).Unsubscribe(OnBulletCollided);
            _bulletPool.ReleaseEntity(bullet);
        }

        ~BulletPoolReleaseController()
        {
            _bulletPool.Given -= OnBulletGiven;
        }
    }
}