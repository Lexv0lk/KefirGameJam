using Game.Scripts.Configs.Models;
using UnityEngine;

namespace Game.Scripts.Configs
{
    [CreateAssetMenu(fileName = "Weapon Change Config", menuName = "Configs/Weapon Change")]
    public class WeaponChangeConfig : ScriptableObject
    {
        [SerializeField] private WeaponConfig _startWeapon;

        public WeaponConfig StartWeapon => _startWeapon;
    }
}