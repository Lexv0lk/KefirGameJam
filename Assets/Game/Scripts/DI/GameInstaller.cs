using Atomic.Objects;
using Game.Audio;
using Game.Scripts.Controllers;
using Game.Scripts.Fabrics;
using Game.Scripts.Growing;
using Game.Scripts.LevelGeneration;
using Game.Scripts.Loot;
using Game.Scripts.Models;
using Game.Scripts.Pools;
using Game.Scripts.UI.Controllers;
using Game.Scripts.UI.Views;
using Game.Scripts.Utilities;
using Game.Settings;
using UnityEngine;
using Zenject;

namespace Game.Scripts.DI
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private AtomicEntity _character;
        [SerializeField] private GamePools _gamePools;
        [SerializeField] private EnemySpawnPositions _enemySpawnPositions;
        [SerializeField] private GameInfoView _gameInfoView;
        [SerializeField] private GameEndView _gameEndView;
        
        public override void InstallBindings()
        {
            Container.Bind<Inventory.Inventory>().AsSingle();
            Container.Bind<InputModel>().AsSingle();
            Container.Bind<WaveData>().AsSingle();
            
            Container.Bind<GamePools>().FromInstance(_gamePools).AsSingle();
            Container.Bind<GameInfoView>().FromInstance(_gameInfoView).AsSingle();
            Container.Bind<GameEndView>().FromInstance(_gameEndView).AsSingle();

            Container.Bind<LevelGenerationService>().FromComponentInHierarchy().AsSingle();
            Container.Bind<AudioSceneService>().FromComponentInHierarchy().AsSingle();
            
            Container.Bind<EnemySpawnPositions>().FromInstance(_enemySpawnPositions).AsSingle();
            Container.Bind<RiffleStoreModel>().FromNew().AsSingle();
            Container.Bind<KillCountModel>().FromNew().AsSingle();
            
            Container.Bind<Camera>().FromComponentInHierarchy().AsCached();
            
            Container.BindInterfacesTo<PoolBulletFabric>().AsSingle();

            Container.BindInterfacesAndSelfTo<AtomicEntity>().FromInstance(_character).AsCached();
            
            Container.BindInterfacesAndSelfTo<InputMoveController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<InputMouseRotateController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ShootController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemySpawnController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemyByDistanceDestroyController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<WeaponChangeController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BulletCollisionObserver>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<LootSpawnController>().AsSingle();
            Container.BindInterfacesAndSelfTo<LootCollectObserver>().AsSingle();
                
            Container.Bind<BulletPoolReleaseController>().AsSingle().NonLazy();
            Container.Bind<EnemyDeathObserver>().AsSingle().NonLazy();
            Container.Bind<PlayerDeathObserver>().AsSingle().NonLazy();
            Container.Bind<GameInfoViewAdapter>().AsSingle().NonLazy();

            Container.Bind<PartLevelConnector>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelGenerator>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<GardenController>().AsSingle();
            Container.BindInterfacesAndSelfTo<AudioController>().AsSingle();

            Container.BindInterfacesAndSelfTo<WavesController>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<ApplicationExitController>().AsSingle().NonLazy();
        }
    }
}