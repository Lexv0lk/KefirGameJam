using UnityEngine;

namespace Game.Scripts.Pools
{
    public class GamePools : MonoBehaviour
    {
        [SerializeField] private AtomicEntityPool _bulletPool;
        [SerializeField] private AtomicEntityPool _enemyPool;

        public IAtomicEntityPool BulletPool => _bulletPool;
        public IAtomicEntityPool EnemyPool => _enemyPool;
    }
}