using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Loot;
using Game.Scripts.Models;
using Game.Scripts.Pools;
using Game.Scripts.Tech;

namespace Game.Scripts.Controllers
{
    public class EnemyDeathObserver
    {
        private readonly KillCountModel _killCountModel;
        private readonly LootSpawnController _lootSpawnController;
        private readonly IAtomicEntityPool _enemyPool;

        public EnemyDeathObserver(GamePools gamePools, KillCountModel killCountModel,
            LootSpawnController lootSpawnController)
        {
            _killCountModel = killCountModel;
            _lootSpawnController = lootSpawnController;
            _enemyPool = gamePools.EnemyPool;

            _enemyPool.Given += OnEnemyGiven;
        }

        private void OnEnemyGiven(AtomicEntity enemy)
        {
            enemy.Get<IAtomicEvent<AtomicEntity>>(LifeAPI.DIE_EVENT).Subscribe(EnemyDiedInAnimation);
            enemy.Get<IAtomicEvent<AtomicEntity>>(LifeAPI.DIE_ACTION).Subscribe(EnemyDied);
        }
        
        private void EnemyDied(AtomicEntity enemy)
        {
            enemy.Get<IAtomicEvent<AtomicEntity>>(LifeAPI.DIE_ACTION).Unsubscribe(EnemyDied);
            _killCountModel.Kills.Value++;
            _lootSpawnController.TrySpawn(enemy.transform.position);
        }

        private void EnemyDiedInAnimation(AtomicEntity enemy)
        {
            enemy.Get<IAtomicEvent<AtomicEntity>>(LifeAPI.DIE_EVENT).Unsubscribe(EnemyDiedInAnimation);
            _enemyPool.ReleaseEntity(enemy);
        }

        ~EnemyDeathObserver()
        {
            _enemyPool.Given -= OnEnemyGiven;
        }
    }
}