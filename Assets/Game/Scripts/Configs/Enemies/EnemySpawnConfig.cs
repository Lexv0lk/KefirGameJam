using UnityEngine;

namespace Game.Scripts.Configs.Enemies
{
    [CreateAssetMenu(fileName = "Enemy Spawn Config", menuName = "Configs/Enemy Spawn")]
    public class EnemySpawnConfig : ScriptableObject
    {
        [SerializeField] private float _delay;
        [SerializeField] private float _minDistance;
        [SerializeField] private float _maxDistance;

        public float Delay => _delay;
        public float MinDistance => _minDistance;
        public float MaxDistance => _maxDistance;
    }
}