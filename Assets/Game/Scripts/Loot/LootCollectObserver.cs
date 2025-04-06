using System;
using Zenject;

namespace Game.Scripts.Loot
{
    public class LootCollectObserver : IInitializable, IDisposable
    {
        private readonly LootSpawnController _spawnController;
        private readonly Inventory.Inventory _inventory;

        public LootCollectObserver(LootSpawnController spawnController, Inventory.Inventory inventory)
        {
            _spawnController = spawnController;
            _inventory = inventory;
        }
        
        public void Initialize()
        {
            _spawnController.Spawned += OnSpawned;
        }

        private void OnSpawned(CollectableLootView view)
        {
            view.Collected += OnCollected;
        }

        private void OnCollected(CollectableLootView view, LootConfig loot)
        {
            view.Collected -= OnCollected;
            _inventory.Add(loot);
        }

        public void Dispose()
        {
            _spawnController.Spawned -= OnSpawned;
        }
    }
}