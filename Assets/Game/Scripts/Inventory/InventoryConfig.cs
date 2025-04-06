using System.Collections.Generic;
using Game.Scripts.Loot;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Game.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "Inventory Config", menuName = "Configs/Inventory")]
    public class InventoryConfig : SerializedScriptableObject
    {
        [OdinSerialize] private Dictionary<LootConfig, int> _itemMaximals = new();

        public IReadOnlyDictionary<LootConfig, int> ItemMaximals => _itemMaximals;
    }
}