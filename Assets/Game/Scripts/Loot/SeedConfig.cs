using UnityEngine;

namespace Game.Scripts.Loot
{
    [CreateAssetMenu(fileName = "Seed Config", menuName = "Loot/Seed")]
    public class SeedConfig : LootConfig, IRareble
    {
        [SerializeField] private Rarity _rarity;

        public Rarity Rarity => _rarity;
    }
}