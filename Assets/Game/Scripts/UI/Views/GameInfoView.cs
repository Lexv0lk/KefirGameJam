using Game.Scripts.UI.Presenters;
using TMPro;
using UnityEngine;

namespace Game.Scripts.UI.Views
{
    public class GameInfoView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _bulletsLeft;
        [SerializeField] private TMP_Text _hitPoints;
        [SerializeField] private TMP_Text _kills;
        
        private IGameInfoViewPresenter _gameInfoViewPresenter;
        
        public void Compose(IGameInfoViewPresenter gameInfoViewPresenter)
        {
            _gameInfoViewPresenter = gameInfoViewPresenter;
            
            OnBulletsLeftChanged(_gameInfoViewPresenter.BulletsLeft.Value);
            OnHitPointsChanged(_gameInfoViewPresenter.HitPoints.Value);
            OnKillsChanged(_gameInfoViewPresenter.KillCount.Value);
            
            _gameInfoViewPresenter.BulletsLeft.Subscribe(OnBulletsLeftChanged);
            _gameInfoViewPresenter.HitPoints.Subscribe(OnHitPointsChanged);
            _gameInfoViewPresenter.KillCount.Subscribe(OnKillsChanged);
        }

        private void OnDestroy()
        {
            _gameInfoViewPresenter.BulletsLeft.Unsubscribe(OnBulletsLeftChanged);
            _gameInfoViewPresenter.HitPoints.Unsubscribe(OnHitPointsChanged);
            _gameInfoViewPresenter.KillCount.Unsubscribe(OnKillsChanged);
        }

        private void OnBulletsLeftChanged(string newVal)
        {
            _bulletsLeft.text = newVal;
        }
        
        private void OnHitPointsChanged(string newVal)
        {
            _hitPoints.text = newVal;
        }
        
        private void OnKillsChanged(string newVal)
        {
            _kills.text = newVal;
        }
    }
}