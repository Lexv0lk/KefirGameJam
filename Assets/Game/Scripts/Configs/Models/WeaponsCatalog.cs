using System;
using Game.Scripts.Loot;
using Game.Scripts.Utilities;
using UnityEngine;

namespace Game.Scripts.Configs.Models
{
    [CreateAssetMenu(fileName = "Weapons Catalog", menuName = "Catalogs/Weapons", order = 0)]
    public class WeaponsCatalog : RarityCatalog<WeaponPack>
    {
        
    }

    [Serializable]
    public class WeaponPack : IRareble
    {
        [SerializeField] private WeaponConfig[] _weapons;
        [SerializeField] private Rarity _rarity;

        public WeaponConfig[] Weapons => _weapons;
        public Rarity Rarity => _rarity;
    }
}