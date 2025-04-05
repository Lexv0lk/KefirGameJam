using UnityEngine;

namespace Game.Scripts.Configs.Models
{
    [CreateAssetMenu(fileName = "Shotgun Weapon Config", menuName = "Configs/ShotgunWeapon")]
    public class ShotgunWeaponConfig : WeaponConfig
    {
        [SerializeField] private int _shotBulletsCount = 6;
        [SerializeField] private float _shootAngle = 45f;

        public int ShotBulletsCount => _shotBulletsCount;
        public float ShootAngle => _shootAngle;
    }
}