using System.Threading;
using Atomic.Elements;
using Atomic.Objects;
using Cysharp.Threading.Tasks;
using Game.Scripts.Configs.Enemies;
using Game.Scripts.Pools;
using Game.Scripts.Tech;
using Game.Scripts.Utilities;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Controllers
{
    public class EnemySpawnController : IInitializable
    {
        private readonly EnemySpawnConfig _config;
        private readonly EnemySpawnPositions _enemySpawnPositions;
        private readonly AtomicEntity _player;
        private readonly IAtomicEntityPool _pool;

        private CancellationTokenSource _currentCancellationToken;

        public EnemySpawnController(EnemySpawnConfig config,
            GamePools gamePools, EnemySpawnPositions enemySpawnPositions, AtomicEntity player)
        {
            _config = config;
            _enemySpawnPositions = enemySpawnPositions;
            _player = player;
            _pool = gamePools.EnemyPool;
        }
        
        public void Initialize()
        {
            Enable();
        }

        public void Enable()
        {
            if (_currentCancellationToken != null)
                _currentCancellationToken.Dispose();
            
            _currentCancellationToken = new CancellationTokenSource();
            StartSpawning(_currentCancellationToken).Forget();
        }

        public void Disable()
        {
            _currentCancellationToken.Cancel();
        }

        private async UniTaskVoid StartSpawning(CancellationTokenSource cancellationTokenSource)
        {
            while (true)
            {
                if (cancellationTokenSource.IsCancellationRequested)
                    break;
                
                AtomicEntity enemy = _pool.GetEntity();
                enemy.transform.position = _enemySpawnPositions.GetRandomPosition();
                
                enemy.Get<IAtomicVariable<AtomicEntity>>(EnemyAPI.TARGET).Value = _player;
                
                await UniTask.WaitForSeconds(_config.Delay, cancellationToken: cancellationTokenSource.Token);
            }
        }

        ~EnemySpawnController()
        {
            if (_currentCancellationToken != null)
                _currentCancellationToken.Dispose();
        }
    }
}