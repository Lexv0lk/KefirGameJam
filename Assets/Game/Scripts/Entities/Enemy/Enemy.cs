using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using Game.Scripts.Tech;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Entities
{
    public class Enemy : AtomicObject
    {
        [Get(MoveAPI.FORWARD_DIRECTION)]
        public IAtomicVariable<Vector3> ForwardDirection => _core.RotateComponent.ForwardDirection;

        [Get(LifeAPI.TAKE_DAMAGE_ACTION)]
        public IAtomicAction<int> TakeDamageAction => _core.LifeComponent.TakeDamageAction;

        [Get(LifeAPI.HEALTH)] 
        public IAtomicValueObservable<int> Health => _core.LifeComponent.HealthAmount;

        [Get(LifeAPI.IS_DEAD)] 
        public IAtomicVariable<bool> IsDead => _core.LifeComponent.IsDead;

        [Get(LifeAPI.DIE_ACTION)] 
        public AtomicEvent<AtomicEntity> DieAction;
        
        [Get(LifeAPI.DIE_EVENT)] 
        public AtomicEvent<AtomicEntity> DieEvent;

        [Get(TechAPI.RESET_ACTION)] 
        public IAtomicAction ResetAction => new AtomicAction(Reset);

        [Get(EnemyAPI.TARGET)] 
        public AtomicVariable<AtomicEntity> Target;
        
        [SerializeField] private EnemyCore _core;
        [SerializeField] private EnemyAnimation _enemyAnimation;
        [SerializeField] private EnemyVfx _enemyVfx;
        
        private void Awake()
        {
            AtomicFunction<Vector3> rootPosition = new AtomicFunction<Vector3>(GetPosition);
            
            _core.Compose(rootPosition, Target);
            _enemyAnimation.Compose(_core, Target, InvokeDieAnimationEvent);
            _enemyVfx.Compose(_core);
            
            foreach (var mechanic in _core.GetMechanics())
                AddLogic(mechanic);
            
            foreach (var mechanic in _enemyAnimation.GetMechanics())
                AddLogic(mechanic);
            
            _core.LifeComponent.IsDead.Subscribe(OnDeadStateChanged);
        }

        private void OnEnable()
        {
            Enable();
        }

        private void Update()
        {
            OnUpdate(Time.deltaTime);
        }

        public void Reset()
        {
            _core.AttackComponent.Reset();
            _core.LifeComponent.Reset();
            Target.Value = null;
        }

        private void OnDisable()
        {
            Disable();
        }

        private void OnDestroy()
        {
            _core.LifeComponent.IsDead.Unsubscribe(OnDeadStateChanged);
            _enemyAnimation.Dispose();
            _enemyVfx.Dispose();
        }

        private void OnDeadStateChanged(bool isDead)
        {
            if (isDead)
                DieAction.Invoke(this);
        }

        private void InvokeDieAnimationEvent()
        {
            DieEvent.Invoke(this);
        }

        private Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}