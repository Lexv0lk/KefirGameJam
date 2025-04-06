using Game.Scripts.UI.Presenters;
using Game.Scripts.UI.Views;
using UnityEngine;
using Zenject;

namespace Game.Scripts.UI.Controllers
{
    public class InventoryViewAdapter : MonoBehaviour
    {
        [SerializeField] private InventoryView _view;
        
        private DiContainer _diContainter;

        [Inject]
        private void Init(DiContainer diContainer)
        {
            _diContainter = diContainer;
        }

        private void Awake()
        {
            _view.Initialize(_diContainter.Instantiate<InventoryViewPresenter>());
        }
    }
}