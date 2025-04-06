using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Loot
{
    [Serializable]
    public class LootMetadata
    {
        [SerializeField, PreviewField] private Sprite _icon;
        [SerializeField] private CollectableLootView _prefab;

        public Sprite Icon => _icon;
        public CollectableLootView Prefab => _prefab;
    }
}