using Atomic.Objects;
using UnityEngine;

namespace Game.Scripts.Configs.Models
{
    [CreateAssetMenu(fileName = "Weapon Config", menuName = "Configs/Weapon")]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField] private int _ammoAmount;
        [SerializeField] private float _reloadTime;
        [SerializeField] private int _damage;
        [SerializeField] private AtomicEntity _bulletPrefab;

        public int AmmoAmount => _ammoAmount;
        public float ReloadTime => _reloadTime;
        public int Damage => _damage;
        public AtomicEntity BulletPrefab => _bulletPrefab;
    }
}