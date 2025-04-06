using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Growing
{
    public class GardenView : MonoBehaviour
    {
        [SerializeField] private GardenSlotView[] _slotViews;
        [SerializeField] private SeedSlotView[] _seedViews;

        [Space] 
        
        [SerializeField] private Image _waterFill;
        [SerializeField] private GameObject _seedsRoot;

        [Header("Actions")] 
        [SerializeField] private Button _waterButton;
        [SerializeField] private Button _deleteButton;
        [SerializeField] private Button _collectButton;
        
        private GardenViewPresenter _presenter;

        public void Initialize(GardenViewPresenter presenter)
        {
            _presenter = presenter;
            
            for (int i = 0; i < _slotViews.Length; i++)
                _slotViews[i].Initialize(_presenter.GardenSlots[i]);

            for (int i = 0; i < _seedViews.Length; i++)
                _seedViews[i].Initialize(_presenter.SeedSlots[i]);
            
            presenter.WaterRequest.BindTo(_waterButton).AddTo(this);
            presenter.RemoveRequest.BindTo(_deleteButton).AddTo(this);
            presenter.CollectRequest.BindTo(_collectButton).AddTo(this);
            
            presenter.WaterLeft.Subscribe((part) => _waterFill.fillAmount = part).AddTo(this);
        }

        public void Close()
        {
            _presenter.ExitEvent.Execute();
            gameObject.SetActive(false);
        }

        public void Open()
        {
            foreach (var slotView in _slotViews)
                slotView.ForceUpdateFill();
            
            gameObject.SetActive(true);
        }
    }
}