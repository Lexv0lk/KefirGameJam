using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Scripts.Loot
{
    public class LootSpawnController : ITickable
    {
        private readonly LootSpawnConfig _config;
        private readonly Inventory.Inventory _inventory;
        
        private readonly HashSet<Rarity> _possibleRarities = new HashSet<Rarity>()
        {
            Rarity.Normal
        };
        
        private float _tickedTime;

        public event Action<CollectableLootView> Spawned; 

        public LootSpawnController(LootSpawnConfig config, Inventory.Inventory inventory)
        {
            _config = config;
            _inventory = inventory;
        }
        
        public void Tick()
        {
            if (_tickedTime >= _config.DisappearTimings.Keys.Last())
                return;
            
            if (_tickedTime >= _config.DisappearTimings.Keys.First())
            {
                foreach (var (timing, rarity) in _config.DisappearTimings)
                {
                    if (timing <= _tickedTime)
                        _possibleRarities.Remove(rarity);
                }
            }
            else
            {
                foreach (var (timing, rarity) in _config.AppearTimings)
                {
                    if (timing <= _tickedTime)
                        _possibleRarities.Add(rarity);
                }
            }

            _tickedTime += Time.deltaTime;
        }
        
        public void TrySpawn(Vector3 pos)
        {
            int waterAmount = Random.Range(_config.MinWaterAmount, _config.MaxWaterAmount + 1);
            _inventory.Add(_config.WaterLoot, waterAmount);
            
            if (Random.Range(0f, 1f) > _config.MobLootChance)
                return;

            var rarity = GetRandomRarity();
            var seedConfig = _config.SeedsCatalog.GetItem(rarity);

            CollectableLootView lootInstance =
                GameObject.Instantiate(seedConfig.Metadata.Prefab, pos, Quaternion.identity);
            lootInstance.ConnectTo(seedConfig);
            
            Spawned?.Invoke(lootInstance);
        }

        private Rarity GetRandomRarity()
        {
            if (_possibleRarities.Count == 1)
                return _possibleRarities.First();

            var raritiesList = _possibleRarities.OrderBy(x => x).ToList();
            float chance = Random.Range(0f, 1f);
            float[] chances = _config.LootChances[raritiesList.Count];
            float acumChance = 0;

            for (int i = 0; i < raritiesList.Count; i++)
            {
                acumChance += chances[i] / 100f;
                
                if (chance <= acumChance)
                    return raritiesList[i];
            }

            throw new IndexOutOfRangeException("INVALID RARITY CHANCE CALCULATION");
        }
    }
}