using System;
using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Configs.Controllers;
using Game.Scripts.Pools;
using Game.Scripts.Tech;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Controllers
{
    public class BulletCollisionObserver : IInitializable, IDisposable
    {
        private readonly BulletCollisionConfig _config;
        private readonly AtomicPrefabsPoolSystem _bulletPool;

        private Collider[] _explosionTargets = new Collider[30];

        public BulletCollisionObserver(GamePools gamePools, BulletCollisionConfig config)
        {
            _config = config;
            _bulletPool = gamePools.BulletPool;
        }

        public void Initialize()
        {
            _bulletPool.Given += OnBulletGiven;
            _bulletPool.Released += OnReleased;
        }

        private void OnReleased(AtomicEntity bullet)
        {
            bullet.Get<IAtomicEvent<AtomicEntity>>(PhysicsAPI.COLLIDE_EVENT).Unsubscribe(OnBulletCollided);
            bullet.Get<IAtomicEvent<AtomicEntity, IAtomicEntity>>(ShootAPI.HIT_EVENT).Unsubscribe(OnBulletHitted);
        }

        private void OnBulletGiven(AtomicEntity bullet)
        {
            bullet.Get<IAtomicEvent<AtomicEntity>>(PhysicsAPI.COLLIDE_EVENT).Subscribe(OnBulletCollided);
            bullet.Get<IAtomicEvent<AtomicEntity, IAtomicEntity>>(ShootAPI.HIT_EVENT).Subscribe(OnBulletHitted);
        }

        private void OnBulletCollided(AtomicEntity bullet)
        {
            bullet.Get<IAtomicEvent<AtomicEntity>>(PhysicsAPI.COLLIDE_EVENT).Unsubscribe(OnBulletCollided);

            if (bullet.TryGet<IAtomicValue<float>>(ShootAPI.EXPLOSION_RANGE, out var explosionRange))
            {
                int damage = bullet.Get<IAtomicValue<int>>(ShootAPI.DAMAGE).Value;
                
                var size = Physics.OverlapSphereNonAlloc(bullet.transform.position, explosionRange.Value,
                    _explosionTargets, _config.EnemyMask);

                var particles = bullet.Get<IAtomicValue<ParticleSystem>>(ShootAPI.EXPLOSION_EFFECT).Value;
                var effectPosition = new Vector3(bullet.transform.position.x, 0, bullet.transform.position.z);
                GameObject.Instantiate(particles, effectPosition, Quaternion.identity);

                for (int i = 0; i < size; i++)
                {
                    if (_explosionTargets[i].TryGetComponent(out IAtomicEntity atomicEntity))
                    {
                        if (atomicEntity.TryGet<IAtomicAction<int>>(LifeAPI.TAKE_DAMAGE_ACTION, out var action))
                            action.Invoke(damage);
                    }
                }
            }
        }

        private void OnBulletHitted(AtomicEntity bullet, IAtomicEntity target)
        {
            if (bullet.TryGet<IAtomicValue<float>>(ShootAPI.EXPLOSION_RANGE, out var _))
                return;
            
            if (target.TryGet<IAtomicAction<int>>(LifeAPI.TAKE_DAMAGE_ACTION, out var action))
                action.Invoke(bullet.Get<IAtomicValue<int>>(ShootAPI.DAMAGE).Value);
        }

        public void Dispose()
        {
            _bulletPool.Given -= OnBulletGiven;
            _bulletPool.Released -= OnReleased;
        }
    }
}