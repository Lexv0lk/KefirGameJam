using System;
using Game.Scripts.UI.Presenters;
using Game.Scripts.UI.Views;
using UnityEngine;
using Zenject;

namespace Game.Scripts.UI.Controllers
{
    public class HUDViewAdapter : MonoBehaviour
    {
        [SerializeField] private HUDView _view;
        
        private DiContainer _diContainter;

        [Inject]
        private void Init(DiContainer diContainer)
        {
            _diContainter = diContainer;
        }

        private void Awake()
        {
            _view.Initialize(_diContainter.Instantiate<HUDViewPresenter>());
        }
    }
}