using System;
using Cysharp.Threading.Tasks;
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
        
        [SerializeField] private GameObject _seedsRoot;
        [SerializeField] private Button _cancelButton;

        [Header("Actions")] 
        [SerializeField] private Button _waterButton;
        [SerializeField] private Button _deleteButton;
        [SerializeField] private Button _collectButton;
        [SerializeField] private Button _plantButton;
        
        private GardenViewPresenter _presenter;

        public void Initialize(GardenViewPresenter presenter)
        {
            _presenter = presenter;
            
            for (int i = 0; i < _slotViews.Length; i++)
                _slotViews[i].Initialize(_presenter.GardenSlots[i]);

            for (int i = 0; i < _seedViews.Length; i++)
                _seedViews[i].Initialize(_presenter.SeedSlots[i]);

            _cancelButton.onClick.AddListener(OnCancelButtonClicked);
            _plantButton.onClick.AddListener(OnPlantClicked);

            presenter.WaterRequest.BindTo(_waterButton).AddTo(this);
            presenter.RemoveRequest.BindTo(_deleteButton).AddTo(this);
            presenter.CollectRequest.BindTo(_collectButton).AddTo(this);
        }

        public void Close()
        {
            _presenter.ExitEvent.Execute();
            gameObject.SetActive(false);
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        private void OnPlantClicked()
        {
            Debug.Log($"PLANTS");
            _seedsRoot.SetActive(!_seedsRoot.activeInHierarchy);
        }

        private void OnCancelButtonClicked()
        {
            _presenter.ExitEvent.Execute();
        }

        private void OnDestroy()
        {
            _cancelButton.onClick.RemoveListener(OnCancelButtonClicked);
            _plantButton.onClick.RemoveListener(OnPlantClicked);
        }
    }
}