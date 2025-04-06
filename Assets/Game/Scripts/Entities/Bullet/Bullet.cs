using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Components;
using Game.Scripts.Tech;
using UnityEngine;

namespace Game.Scripts.Entities
{
    public class Bullet : AtomicObject
    {
        [Get(MoveAPI.MOVE_DIRECTION)]
        public IAtomicVariable<Vector3> MoveDirection => _rigidbodyMoveComponent.Direction;
        
        [Get(PhysicsAPI.COLLIDE_EVENT)] 
        public AtomicEvent<AtomicEntity> Collided;

        [Get(ShootAPI.DAMAGE)] 
        public IAtomicVariable<int> Damage => _damage;

        [Get(ShootAPI.HIT_EVENT)] 
        public AtomicEvent<AtomicEntity, IAtomicEntity> HitEvent;

        [SerializeField] private RigidbodyMoveComponent _rigidbodyMoveComponent;
        [SerializeField] private AtomicVariable<int> _damage = new(1);

        private void Awake()
        {
            _rigidbodyMoveComponent.Compose();

            foreach (var mechanic in _rigidbodyMoveComponent.GetMechanics())
                AddLogic(mechanic);
        }

        private void OnEnable()
        {
            Enable();
        }

        private void Update()
        {
            OnUpdate(Time.deltaTime);
        }

        private void OnDisable()
        {
            Disable();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IAtomicEntity atomicEntity))
            {
                HitEvent.Invoke(this, atomicEntity);
                OnCollided(atomicEntity);
            }

            Collided.Invoke(this);
        }

        protected virtual void OnCollided(IAtomicEntity atomicEntity)
        {
            if (atomicEntity.TryGet<IAtomicAction<int>>(LifeAPI.TAKE_DAMAGE_ACTION, out var action))
                action.Invoke(_damage.Value);
        }
    }
}