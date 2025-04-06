using UnityEngine;

namespace Game.Scripts.Loot
{
    public abstract class LootConfig : ScriptableObject
    {
        [SerializeField] private LootMetadata _metadata;

        public LootMetadata Metadata => _metadata;
    }
}