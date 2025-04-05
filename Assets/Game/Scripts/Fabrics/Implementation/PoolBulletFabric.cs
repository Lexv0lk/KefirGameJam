using Atomic.Objects;
using Game.Scripts.Pools;

namespace Game.Scripts.Fabrics
{
    public class PoolBulletFabric : IBulletFabric
    {
        private readonly AtomicPrefabsPoolSystem _pool;

        public PoolBulletFabric(GamePools gamePools)
        {
            _pool = gamePools.BulletPool;
        }

        public AtomicEntity GetBullet(AtomicEntity prefab)
        {
            AtomicEntity bullet = _pool.GetEntity(prefab);
            return bullet;
        }
    }
}