using System.Collections.Generic;
using Game.Scripts.Loot;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Game.Scripts.Growing
{
    [CreateAssetMenu(fileName = "Garden Config", menuName = "Configs/Garden")]
    public class GardenControlConfig : SerializedScriptableObject
    {
        [SerializeField] private float _waterPriceMultiplier = 0.5f;
        [SerializeField] private float _maturationAmmoRefillMultiplier = 0.5f;
        [SerializeField] private int _gardenPlaceCount = 3;
        [SerializeField] private LootConfig _waterLootConfig;
        [OdinSerialize] private Dictionary<Rarity, GrowData> _growDatas = new();
        
        public float WaterPriceMultiplier => _waterPriceMultiplier;
        public float MaturationAmmoRefillMultiplier => _maturationAmmoRefillMultiplier;
        public int GardenPlaceCount => _gardenPlaceCount;
        public IReadOnlyDictionary<Rarity, GrowData> GrowDatas => _growDatas;
        public LootConfig WaterLootConfig => _waterLootConfig;
    }
}