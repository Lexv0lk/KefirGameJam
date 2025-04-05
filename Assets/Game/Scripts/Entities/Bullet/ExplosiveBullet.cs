using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Tech;
using UnityEngine;

namespace Game.Scripts.Entities
{
    public class ExplosiveBullet : Bullet
    {
        [Get(ShootAPI.EXPLOSION_RANGE)] 
        public IAtomicValue<float> ExplosionRange => _explosionRange;

        [Get(ShootAPI.EXPLOSION_EFFECT)] 
        public IAtomicValue<ParticleSystem> ExplosionEffect => _explosionEffect;
        
        [SerializeField] private AtomicVariable<float> _explosionRange;
        [SerializeField] private AtomicVariable<ParticleSystem> _explosionEffect;
    }
}