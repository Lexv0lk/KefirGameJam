using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Tech;
using Game.Scripts.UI.Views;
using UnityEngine;

namespace Game.Scripts.Controllers
{
    public class PlayerDeathObserver
    {
        private readonly ShootController _shootController;
        private readonly GameEndView _gameEndView;
        private readonly EnemySpawnController _spawnController;
        private readonly AmmunitionRefillController _refillController;
        private readonly IAtomicObservable<bool> _isDeadObservable;

        public PlayerDeathObserver(IAtomicEntity player, ShootController shootController, GameEndView gameEndView, 
            EnemySpawnController spawnController, AmmunitionRefillController refillController)
        {
            _shootController = shootController;
            _gameEndView = gameEndView;
            _spawnController = spawnController;
            _refillController = refillController;
            _isDeadObservable = player.Get<IAtomicObservable<bool>>(LifeAPI.IS_DEAD);
            _isDeadObservable.Subscribe(OnPlayerDied);
        }

        private void OnPlayerDied(bool isDead)
        {
            if (isDead == false)
                return;

            _shootController.Disable();
            _spawnController.Disable();
            _refillController.Disable();
            _gameEndView.gameObject.SetActive(true);
        }

        ~PlayerDeathObserver()
        {
            _isDeadObservable.Unsubscribe(OnPlayerDied);
        }
    }
}