using System;
using Atomic.Objects;
using Game.Scripts.Loot;
using UnityEngine;

namespace Game.Scripts.Configs.Models
{
    [CreateAssetMenu(fileName = "Weapon Config", menuName = "Configs/Weapon")]
    public class WeaponConfig : ScriptableObject, IRareble
    {
        [SerializeField] private WeaponMetadata _metadata;
        [SerializeField] private WeaponType _weaponType;
        [SerializeField] private Rarity _rarity;
        [SerializeField] private int _ammoAmount;
        [SerializeField] private float _reloadTime;
        [SerializeField] private int _damage;
        [SerializeField] private AtomicEntity _bulletPrefab;

        public int AmmoAmount => _ammoAmount;
        public float ReloadTime => _reloadTime;
        public int Damage => _damage;
        public WeaponMetadata Metadata => _metadata;
        public Rarity Rarity => _rarity;
        public AtomicEntity BulletPrefab => _bulletPrefab;
        public WeaponType WeaponType => _weaponType;
    }

    [Serializable]
    public enum WeaponType
    {
        Riffle = 0,
        Shotgun = 1,
        Rocketgun = 2,
        MAX = 3
    }
}