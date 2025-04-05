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

        [SerializeField] private RigidbodyMoveComponent _rigidbodyMoveComponent;
        [SerializeField] private int _damage = 1;

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
                if (atomicEntity.TryGet<IAtomicAction<int>>(LifeAPI.TAKE_DAMAGE_ACTION, out var action))
                    action.Invoke(_damage);
            
            Collided.Invoke(this);
        }
    }
}