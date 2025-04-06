using UnityEngine;

namespace Game.Scripts.Configs.Controllers
{
    [CreateAssetMenu(fileName = "Bullet Collision Config", menuName = "Configs/BulletCollision", order = 0)]
    public class BulletCollisionConfig : ScriptableObject
    {
        [SerializeField] private LayerMask _enemyMask;

        public LayerMask EnemyMask => _enemyMask;
    }
}