using System;
using Game.Scripts.UI.Presenters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Views
{
    public class GameInfoView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _kills;
        [SerializeField] private Image _healthBar;
        [SerializeField] private float _healthBarSpeed = 1f;
        
        private IGameInfoViewPresenter _gameInfoViewPresenter;
        private float _healthTarget;
        
        public void Compose(IGameInfoViewPresenter gameInfoViewPresenter)
        {
            _gameInfoViewPresenter = gameInfoViewPresenter;
            
            OnHealthChanged(_gameInfoViewPresenter.Health.Value);
            OnKillsChanged(_gameInfoViewPresenter.KillCount.Value);
            
            _gameInfoViewPresenter.Health.Subscribe(OnHealthChanged);
            _gameInfoViewPresenter.KillCount.Subscribe(OnKillsChanged);
        }

        private void OnDestroy()
        {
            _gameInfoViewPresenter.Health.Unsubscribe(OnHealthChanged);
            _gameInfoViewPresenter.KillCount.Unsubscribe(OnKillsChanged);
        }

        private void Update()
        {
            if (Math.Abs(_healthBar.fillAmount - _healthTarget) > 0.01f)
                _healthBar.fillAmount = Mathf.MoveTowards(_healthBar.fillAmount, _healthTarget, Time.deltaTime * _healthBarSpeed);
        }

        private void OnHealthChanged(float newVal)
        {
            _healthTarget = newVal;
        }
        
        private void OnKillsChanged(string newVal)
        {
            _kills.text = newVal;
        }
    }
}