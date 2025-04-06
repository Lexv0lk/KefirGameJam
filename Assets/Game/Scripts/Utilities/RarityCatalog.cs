using System.Collections.Generic;
using Game.Scripts.Loot;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Game.Scripts.Utilities
{
    public abstract class RarityCatalog<TItem> : SerializedScriptableObject where TItem : class, IRareble
    {
        [OdinSerialize] private Dictionary<Rarity, TItem> _items;

        public TItem GetItem(Rarity rarity)
        {
            return _items[rarity];
        }
    }
}