using UnityEngine;

namespace Game.Scripts.Configs.Controllers
{
    [CreateAssetMenu(fileName = "Waves Config", menuName = "Configs/Waves", order = 0)]
    public class WavesConfig : ScriptableObject
    {
        [SerializeField] private float _waveDuration = 20;
        [SerializeField] private float _damageMultiplier = 1.3f;
        [SerializeField] private float _healthMultiplier = 1.3f;

        [Header("Start Stats")] 
        [SerializeField] private int _startHealth = 100;
        [SerializeField] private int _startDamage = 15;
        
        public float WaveDuration => _waveDuration;
        public float DamageMultiplier => _damageMultiplier;
        public float HealthMultiplier => _healthMultiplier;
        public int StartHealth => _startHealth;
        public int StartDamage => _startDamage;
    }
}