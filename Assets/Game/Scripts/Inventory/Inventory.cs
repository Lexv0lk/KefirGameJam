using System;
using System.Collections.Generic;
using Game.Scripts.Loot;
using UnityEngine;

namespace Game.Scripts.Inventory
{
    public class Inventory
    {
        private readonly Dictionary<LootConfig, int> _items = new();
        private readonly IReadOnlyDictionary<LootConfig, int> _itemMaximals;
        
        public event Action Changed;

        public Inventory(InventoryConfig config)
        {
            _itemMaximals = config.ItemMaximals;
        }

        public void Add(LootConfig lootConfig, int amount = 1)
        {
            if (_items.ContainsKey(lootConfig) == false)
                _items[lootConfig] = 0;

            _items[lootConfig] += amount;

            if (_itemMaximals.ContainsKey(lootConfig))
                _items[lootConfig] = Math.Min(_items[lootConfig], _itemMaximals[lootConfig]);
            
            Changed?.Invoke();
        }

        public int GetCount(LootConfig loot)
        {
            return _items.ContainsKey(loot) ? _items[loot] : 0;
        }
        
        public int GetMaxCount(LootConfig loot)
        {
            return _itemMaximals.ContainsKey(loot) ? _itemMaximals[loot] : int.MaxValue;
        }

        public void Remove(LootConfig loot, int amount = 1)
        {
            _items[loot] -= amount;
            Changed?.Invoke();
        }
    }
}