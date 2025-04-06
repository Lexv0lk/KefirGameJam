using System.Threading;
using Atomic.Elements;
using Atomic.Objects;
using Cysharp.Threading.Tasks;
using Game.Scripts.Configs.Enemies;
using Game.Scripts.Models;
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
        private readonly WaveData _waveData;
        private readonly IAtomicEntityPool _pool;

        private CancellationTokenSource _currentCancellationToken;

        public EnemySpawnController(EnemySpawnConfig config,
            GamePools gamePools, EnemySpawnPositions enemySpawnPositions, AtomicEntity player, WaveData waveData)
        {
            _config = config;
            _enemySpawnPositions = enemySpawnPositions;
            _player = player;
            _waveData = waveData;
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
                enemy.transform.position = GetRandomPointOnCircle(_player.transform.position, _config.MinDistance);
                
                enemy.Get<IAtomicVariable<AtomicEntity>>(EnemyAPI.TARGET).Value = _player;
                
                enemy.Get<IAtomicVariable<int>>(LifeAPI.MAX_HEALTH).Value = _waveData.CurrentHealth;
                enemy.Get<IAtomicVariable<int>>(LifeAPI.HEALTH).Value = _waveData.CurrentHealth;
                enemy.Get<IAtomicVariable<int>>(ShootAPI.DAMAGE).Value = _waveData.CurrentDamage;
                
                await UniTask.WaitForSeconds(_config.Delay, cancellationToken: cancellationTokenSource.Token);
            }
        }
        
        public static Vector3 GetRandomPointOnCircle(Vector3 center, float radius)
        {
            float angle = Random.Range(0f, 2f * Mathf.PI);
            float x = center.x + Mathf.Cos(angle) * radius;
            float z = center.z + Mathf.Sin(angle) * radius;
            return new Vector3(x, center.y, z);
        }

        ~EnemySpawnController()
        {
            if (_currentCancellationToken != null)
                _currentCancellationToken.Dispose();
        }
    }
}