using Atomic.Elements;
using Atomic.Objects;
using Game.Audio;
using Game.Scripts.Configs.Models;
using Game.Scripts.Fabrics;
using Game.Scripts.Models;
using Game.Scripts.Tech;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Entities
{
    public class Player : AtomicObject
    {
        [Get(MoveAPI.MOVE_DIRECTION)]
        public IAtomicVariable<Vector3> MoveDirection => _core.MoveComponent.Direction;
        
        [Get(MoveAPI.FORWARD_DIRECTION)]
        public IAtomicVariable<Vector3> ForwardDirection => _core.RotateComponent.ForwardDirection;

        [Get(LifeAPI.TAKE_DAMAGE_ACTION)]
        public IAtomicAction<int> TakeDamageAction => _core.LifeComponent.TakeDamageAction;

        [Get(LifeAPI.HEALTH)] 
        public IAtomicValueObservable<int> Health => _core.LifeComponent.HealthAmount;
        
        [Get(LifeAPI.MAX_HEALTH)] 
        public IAtomicValueObservable<int> MaxHealth => _core.LifeComponent.StartHealthAmount;

        [Get(LifeAPI.IS_DEAD)] 
        public IAtomicValueObservable<bool> IsDead => _core.LifeComponent.IsDead;

        [Get(LifeAPI.DIE_ACTION)] 
        public AtomicEvent<AtomicEntity> DieAction;
        
        [Get(LifeAPI.DIE_EVENT)] 
        public AtomicEvent<AtomicEntity> DieEvent;

        [Get(TechAPI.RESET_ACTION)] 
        public IAtomicAction ResetAction => new AtomicAction(Reset);
        
        [Get(ShootAPI.SHOOT_REQUEST)]
        public IAtomicAction ShootRequest => _core.ShootComponent.ShootRequest;

        [Get(ShootAPI.CURRENT_WEAPON)]
        public IAtomicVariable<WeaponConfig> CurrentWeapon => _core.ShootComponent.CurrentWeapon;
        
        [SerializeField] private PlayerCore _core;
        [SerializeField] private PlayerAnimation _playerAnimation;
        [SerializeField] private PlayerVfx _playerVfx;

        private IBulletFabric _bulletFabric;
        private RiffleStoreModel _riffleStoreModel;
        private AudioController _audioController;

        [Inject]
        private void Construct(IBulletFabric fabric, RiffleStoreModel riffleStoreModel, AudioController audioController)
        {
            _bulletFabric = fabric;
            _riffleStoreModel = riffleStoreModel;
            _audioController = audioController;
        }
        
        private void Awake()
        {
            _core.Compose(_bulletFabric, _riffleStoreModel);
            _playerAnimation.Compose(_core, InvokeDieAnimationEvent, transform);
            _playerVfx.Compose(_core, transform, _audioController);
            
            foreach (var mechanic in _core.GetMechanics())
                AddLogic(mechanic);
            
            foreach (var mechanic in _playerAnimation.GetMechanics())
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
            _core.LifeComponent.Reset();
        }

        private void OnDisable()
        {
            Disable();
        }

        private void OnDestroy()
        {
            _core.LifeComponent.IsDead.Unsubscribe(OnDeadStateChanged);
            _core.Dispose();
            
            _playerVfx.Dispose();
            _playerAnimation.Dispose();
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
    }
}