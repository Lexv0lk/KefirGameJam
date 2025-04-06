using Game.Scripts.Configs;
using Game.Scripts.Configs.Controllers;
using Game.Scripts.Configs.Enemies;
using Game.Scripts.Configs.Fabrics;
using Game.Scripts.Configs.Input;
using Game.Scripts.Configs.Models;
using Game.Scripts.Controllers;
using Game.Scripts.Growing;
using Game.Scripts.Inventory;
using Game.Scripts.LevelGeneration;
using Game.Scripts.Loot;
using Game.Settings;
using UnityEngine;
using Zenject;

namespace Game.Scripts.DI
{
    [CreateAssetMenu(fileName = "Configs Installer", menuName = "DI/Configs Installer")]
    public class ConfigsInstaller : ScriptableObjectInstaller
    {
        [Header("Configs")]
        [SerializeField] private InputConfig _inputConfig;
        [SerializeField] private MouseRotationConfig _mouseRotationConfig;
        [SerializeField] private BulletFabricConfig _bulletFabricConfig;
        [SerializeField] private RiffleStoreConfig _riffleStoreConfig;
        [SerializeField] private AmmunitionRefillConfig _ammunitionRefillConfig;
        [SerializeField] private EnemySpawnConfig _enemySpawnConfig;
        [SerializeField] private LevelGenerationConfig _levelGenerationConfig;
        [SerializeField] private WeaponChangeConfig _weaponChangeConfig;
        [SerializeField] private BulletCollisionConfig _bulletCollisionConfig;
        [SerializeField] private LootSpawnConfig _lootSpawnConfig;
        [SerializeField] private GardenControlConfig _gardenControlConfig;
        [SerializeField] private GardenViewConfig _gardenViewConfig;
        [SerializeField] private InventoryConfig _inventoryConfig;
        [SerializeField] private AudioConfig _audioConfig;
        [SerializeField] private WavesConfig _wavesConfig;

        [Header("Catalogs")] 
        [SerializeField] private SeedsCatalog _seedsCatalog;
        [SerializeField] private WeaponsCatalog _weaponsCatalog;

        public override void InstallBindings()
        {
            Container.Bind<InputConfig>().FromInstance(_inputConfig).AsCached();
            Container.Bind<MouseRotationConfig>().FromInstance(_mouseRotationConfig).AsCached();
            Container.Bind<BulletFabricConfig>().FromInstance(_bulletFabricConfig).AsCached();
            Container.Bind<RiffleStoreConfig>().FromInstance(_riffleStoreConfig).AsCached();
            Container.Bind<AmmunitionRefillConfig>().FromInstance(_ammunitionRefillConfig).AsCached();
            Container.Bind<EnemySpawnConfig>().FromInstance(_enemySpawnConfig).AsCached();
            Container.Bind<LevelGenerationConfig>().FromInstance(_levelGenerationConfig).AsCached();
            Container.Bind<WeaponChangeConfig>().FromInstance(_weaponChangeConfig).AsCached();
            Container.Bind<BulletCollisionConfig>().FromInstance(_bulletCollisionConfig).AsCached();
            Container.Bind<LootSpawnConfig>().FromInstance(_lootSpawnConfig).AsCached();
            Container.Bind<GardenControlConfig>().FromInstance(_gardenControlConfig).AsCached();
            Container.Bind<GardenViewConfig>().FromInstance(_gardenViewConfig).AsCached();
            Container.Bind<InventoryConfig>().FromInstance(_inventoryConfig).AsCached();
            Container.Bind<AudioConfig>().FromInstance(_audioConfig).AsCached();
            Container.Bind<WavesConfig>().FromInstance(_wavesConfig).AsCached();
            
            Container.Bind<SeedsCatalog>().FromInstance(_seedsCatalog).AsCached();
            Container.Bind<WeaponsCatalog>().FromInstance(_weaponsCatalog).AsCached();
        }
    }
}