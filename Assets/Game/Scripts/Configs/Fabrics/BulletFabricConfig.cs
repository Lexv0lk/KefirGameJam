using Atomic.Objects;
using UnityEngine;

namespace Game.Scripts.Configs.Fabrics
{
    [CreateAssetMenu(fileName = "Bullet Fabric Config", menuName = "Configs/Bullet Fabric")]
    public class BulletFabricConfig : ScriptableObject
    {
        [SerializeField] private AtomicEntity _bulletPrefab;

        public AtomicEntity BulletPrefab => _bulletPrefab;
    }
}