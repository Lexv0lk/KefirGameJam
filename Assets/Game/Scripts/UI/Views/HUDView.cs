using Game.Scripts.UI.Presenters;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Game.Scripts.UI.Views
{
    public class HUDView : MonoBehaviour
    {
        [SerializeField] private MainGunView _gunView;
        [SerializeField] private GrowStatusView[] _growStatusViews;
        [SerializeField] private Image _waterFill;
        
        public void Initialize(HUDViewPresenter presenter)
        {
            _gunView.Initialize(presenter.MainGunViewPresenter);
            
            for (int i = 0; i < _growStatusViews.Length; i++)
                _growStatusViews[i].Initialize(presenter.GrowStatusViewPresenters[i]);

            presenter.WaterFillAmount.Subscribe((part) => _waterFill.fillAmount = part).AddTo(this);
        }
    }
}