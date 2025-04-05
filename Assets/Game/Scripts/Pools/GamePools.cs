using UnityEngine;

namespace Game.Scripts.Pools
{
    public class GamePools : MonoBehaviour
    {
        [SerializeField] private AtomicPrefabsPoolSystem _bulletPool;
        [SerializeField] private AtomicEntityPool _enemyPool;

        public AtomicPrefabsPoolSystem BulletPool => _bulletPool;
        public IAtomicEntityPool EnemyPool => _enemyPool;
    }
}