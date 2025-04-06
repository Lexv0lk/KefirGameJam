using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Models;
using Game.Scripts.Tech;
using UniRx;

namespace Game.Scripts.UI.Presenters
{
    public class GameInfoViewPresenter : IGameInfoViewPresenter
    {
        private readonly RiffleStoreModel _ammoModel;
        private readonly KillCountModel _killsModel;
        private readonly IAtomicValueObservable<int> _playerHealth;
        private readonly IAtomicValueObservable<int> _playerMaxHealth;
        private readonly CompositeDisposable _disposable = new();

        private AtomicVariable<string> _bulletsLeft = new();
        private AtomicVariable<string> _killCount = new();
        private AtomicVariable<float> _health = new();

        public GameInfoViewPresenter(RiffleStoreModel ammoModel, KillCountModel killsModel, AtomicEntity player)
        {
            _ammoModel = ammoModel;
            _killsModel = killsModel;
            _playerHealth = player.Get<IAtomicValueObservable<int>>(LifeAPI.HEALTH);
            _playerMaxHealth = player.Get<IAtomicValueObservable<int>>(LifeAPI.MAX_HEALTH);
            
            OnAmmoChanged(_ammoModel.AmmunitionAmount.Value);
            OnKillsChanged(_killsModel.Kills.Value);
            OnHitPointsChanged(_playerHealth.Value);
            
            _ammoModel.AmmunitionAmount.Subscribe(OnAmmoChanged).AddTo(_disposable);;
            _killsModel.Kills.Subscribe(OnKillsChanged);
            _playerHealth.Subscribe(OnHitPointsChanged);
        }
        
        public IAtomicValueObservable<string> BulletsLeft => _bulletsLeft;
        public IAtomicValueObservable<float> Health => _health;
        public IAtomicValueObservable<string> KillCount => _killCount;

        private void OnAmmoChanged(int newVal)
        {
            _bulletsLeft.Value = $"BULLETS: {_ammoModel.AmmunitionAmount.Value} / {_ammoModel.MaxAmmunitionAmount.Value}";
        }

        private void OnKillsChanged(int newVal)
        {
            _killCount.Value = $"{newVal}";
        }

        private void OnHitPointsChanged(int newVal)
        {
            _health.Value = (float)newVal / _playerMaxHealth.Value;
        }

        ~GameInfoViewPresenter()
        {
            _disposable.Dispose();
            _killsModel.Kills.Unsubscribe(OnKillsChanged);
            _playerHealth.Unsubscribe(OnHitPointsChanged);
        }
    }
}