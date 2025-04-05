using UnityEngine;

namespace Game.Scripts.Configs.Controllers
{
    [CreateAssetMenu(fileName = "Ammo Refill", menuName = "Configs/Ammo Refill")]
    public class AmmunitionRefillConfig : ScriptableObject
    {
        [SerializeField] private int _refillCount;
        [SerializeField] private int _delay;

        public int RefillCount => _refillCount;
        public int Delay => _delay;
    }
}