using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Game.Scripts.Loot
{
    [CreateAssetMenu(fileName = "Loot Spawn Config", menuName = "Configs/Loot Spawn")]
    public class LootSpawnConfig : SerializedScriptableObject
    {
        [SerializeField] private LootConfig _waterLoot;
        [SerializeField] private SeedsCatalog _seedsCatalog;
        [SerializeField] private int _minWaterAmount;
        [SerializeField] private int _maxWaterAmount;
        [SerializeField] private float _mobLootChance = 0.25f;
        [OdinSerialize] private Dictionary<int, float[]> _lootChances = new();
        [OdinSerialize] private Dictionary<float, Rarity> _appearTimings = new();
        [OdinSerialize] private Dictionary<float, Rarity> _disappearTimings = new();

        public LootConfig WaterLoot => _waterLoot;
        public SeedsCatalog SeedsCatalog => _seedsCatalog;
        public int MinWaterAmount => _minWaterAmount;
        public int MaxWaterAmount => _maxWaterAmount;
        public float MobLootChance => _mobLootChance;
        public IReadOnlyDictionary<int, float[]> LootChances => _lootChances;
        public IReadOnlyDictionary<float, Rarity> AppearTimings => _appearTimings;
        public IReadOnlyDictionary<float, Rarity> DisappearTimings => _disappearTimings;
    }
}