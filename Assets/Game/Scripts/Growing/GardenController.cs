using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Configs.Models;
using Game.Scripts.Loot;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Scripts.Growing
{
    public class GardenController : ITickable
    {
        private readonly GardenControlConfig _config;
        private readonly Inventory.Inventory _inventory;
        private readonly WeaponsCatalog _weaponsCatalog;
        private readonly ReactiveCollection<GrowInfo> _currentGrowings = new();
        
        private readonly HashSet<GrowInfo> _cachedDeleteInfo = new();
        
        public IReadOnlyReactiveCollection<GrowInfo> CurrentGrowings => _currentGrowings;

        public event Action<GrowInfo> Died; 

        public GardenController(GardenControlConfig config, Inventory.Inventory inventory,
            WeaponsCatalog weaponsCatalog)
        {
            _config = config;
            _inventory = inventory;
            _weaponsCatalog = weaponsCatalog;
        }

        public bool IsPlaceLeft()
        {
            return _currentGrowings.Count < _config.GardenPlaceCount;
        }

        public bool CanPlant(SeedConfig seed)
        {
            return IsPlaceLeft() && _inventory.GetCount(seed) > 0 && _inventory.GetCount(_config.WaterLootConfig) >= _config.GrowDatas[seed.Rarity].WaterConsumption;
        }

        public void Collect(GrowInfo growInfo)
        {
            if (_currentGrowings.Contains(growInfo) == false)
            {
                Debug.LogError("NO SUCH GROWING");
                return;
            }
            
            _currentGrowings.Remove(growInfo);
        }

        public void Remove(GrowInfo growing)
        {
            if (_currentGrowings.Contains(growing) == false)
            {
                Debug.LogError("NO SUCH GROWING");
                return;
            }

            _currentGrowings.Remove(growing);
            Died?.Invoke(growing);
        }

        public void Water(GrowInfo growing)
        {
            int waterCost = Mathf.CeilToInt(_config.GrowDatas[growing.Result.Rarity].WaterConsumption * _config.WaterPriceMultiplier);
            
            if (_inventory.GetCount(_config.WaterLootConfig) < waterCost)
            {
                Debug.LogError("Not enough water");
                return;
            }
            
            _inventory.Remove(_config.WaterLootConfig, waterCost);
            growing.MaturationTimeLeft.Value = growing.GrowData.MaturationTime;
        }
        
        public GrowInfo Plant(SeedConfig seed)
        {
            if (CanPlant(seed) == false)
            {
                Debug.LogError("Can't plant seed");
                return null;
            }

            var possibleWeapons = _weaponsCatalog.GetItem(seed.Rarity);
            WeaponType weaponType = (WeaponType)Random.Range(0, (int)WeaponType.MAX);
            var resultWeapon = possibleWeapons.Weapons.First(x => x.WeaponType == weaponType);

            var growInfo = new GrowInfo(resultWeapon, _config.GrowDatas[seed.Rarity]);
            _currentGrowings.Add(growInfo);
            _inventory.Remove(seed);
            _inventory.Remove(_config.WaterLootConfig, _config.GrowDatas[seed.Rarity].WaterConsumption);

            return growInfo;
        }

        public void Tick()
        {
            float deltaTime = Time.deltaTime;
            _cachedDeleteInfo.Clear();

            foreach (var growInfo in _currentGrowings)
            {
                if (growInfo.GrowTimeLeft.Value > 0)
                {
                    growInfo.GrowTimeLeft.Value = Mathf.Max(0, growInfo.GrowTimeLeft.Value - deltaTime);
                }
                else
                {
                    if (growInfo.IsGrowed.Value == false)
                        growInfo.IsGrowed.Value = true;
                    
                    growInfo.MaturationTimeLeft.Value = Mathf.Max(0, growInfo.MaturationTimeLeft.Value - deltaTime);
                    
                    float ammoStep = growInfo.MaximalAmmoCount /
                                     (_config.MaturationAmmoRefillMultiplier * growInfo.GrowData.MaturationTime);
                    ammoStep *= deltaTime;
                    
                    growInfo.AmmoCountAccumulated.Value = Mathf.Min(growInfo.MaximalAmmoCount,
                        growInfo.AmmoCountAccumulated.Value + ammoStep);
                }

                if (growInfo.MaturationTimeLeft.Value == 0)
                {
                    _cachedDeleteInfo.Add(growInfo);
                }
            }

            foreach (var growInfo in _cachedDeleteInfo)
            {
                _currentGrowings.Remove(growInfo);
                Died?.Invoke(growInfo);
            }
        }
    }
}