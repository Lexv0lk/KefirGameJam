using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Configs.Enemies;
using Game.Scripts.Pools;
using Game.Scripts.Tech;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Controllers
{
    public class EnemyByDistanceDestroyController : IInitializable, ITickable
    {
        private readonly GamePools _gamePools;
        private readonly AtomicEntity _player;
        private readonly EnemySpawnConfig _config;
        private readonly HashSet<AtomicEntity> _currentEnemies = new(); 

        public EnemyByDistanceDestroyController(GamePools gamePools, AtomicEntity player, EnemySpawnConfig config)
        {
            _gamePools = gamePools;
            _player = player;
            _config = config;
        }
        
        public void Initialize()
        {
            _gamePools.EnemyPool.Given += OnEnemyGiven;
            _gamePools.EnemyPool.Released += OnEnemyReleased;
        }

        private void OnEnemyReleased(AtomicEntity enemy)
        {
            _currentEnemies.Remove(enemy);
        }

        private void OnEnemyGiven(AtomicEntity enemy)
        {
            _currentEnemies.Add(enemy);
        }

        public void Tick()
        {
            foreach (var enemy in _currentEnemies)
            {
                if (enemy == null)
                    continue;

                if (Vector3.Distance(enemy.transform.position, _player.transform.position) > _config.MaxDistance)
                {
                    _gamePools.EnemyPool.ReleaseEntity(enemy);
                    break;
                }
            }
        }
    }
}