using System;
using System.Collections.Generic;
using Game.Scripts.Loot;
using UnityEngine;

namespace Game.Scripts.Inventory
{
    public class Inventory
    {
        private readonly Dictionary<LootConfig, int> _items = new();

        public event Action Changed; 

        public void Add(LootConfig lootConfig, int amount = 1)
        {
            if (_items.ContainsKey(lootConfig) == false)
                _items[lootConfig] = 0;

            _items[lootConfig] += amount;
            Changed?.Invoke();
            
            Debug.Log("ADDED " + lootConfig.name);
        }

        public int GetCount(LootConfig loot)
        {
            return _items.ContainsKey(loot) ? _items[loot] : 0;
        }

        public void Remove(LootConfig loot, int amount = 1)
        {
            _items[loot] -= amount;
            Changed?.Invoke();
        }
    }
}