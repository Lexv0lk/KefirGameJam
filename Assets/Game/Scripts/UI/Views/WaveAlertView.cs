using DG.Tweening;
using Game.Scripts.Controllers;
using UnityEngine;
using Zenject;

namespace Game.Scripts.UI.Views
{
    public class WaveAlertView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _alert;
        
        private WavesController _wavesController;

        [Inject]
        private void Init(WavesController wavesController)
        {
            _wavesController = wavesController;
        }

        private void OnEnable()
        {
            _wavesController.NewWaveStarted += OnNewWave;
        }

        private void OnDisable()
        {
            _wavesController.NewWaveStarted -= OnNewWave;
        }

        private void OnNewWave()
        {
            Sequence seq = DOTween.Sequence();

            seq.Append(_alert.DOFade(1, .5f));
            seq.AppendInterval(1);
            seq.Append(_alert.DOFade(0, .5f));
        }
    }
}