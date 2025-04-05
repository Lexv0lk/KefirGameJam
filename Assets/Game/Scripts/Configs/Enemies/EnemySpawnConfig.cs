using UnityEngine;

namespace Game.Scripts.Configs.Enemies
{
    [CreateAssetMenu(fileName = "Enemy Spawn Config", menuName = "Configs/Enemy Spawn")]
    public class EnemySpawnConfig : ScriptableObject
    {
        [SerializeField] private float _delay;

        public float Delay => _delay;
    }
}