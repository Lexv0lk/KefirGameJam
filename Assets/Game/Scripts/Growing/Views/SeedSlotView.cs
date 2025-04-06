using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Growing
{
    public class SeedSlotView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _amount;
        [SerializeField] private Button _button;
        
        public void Initialize(SeedSlotViewPresenter presenter)
        {
            _icon.sprite = presenter.Icon;
            presenter.Count.Subscribe(txt => _amount.text = txt).AddTo(this);
            presenter.UseCommand.BindTo(_button).AddTo(this);
        }
    }
}