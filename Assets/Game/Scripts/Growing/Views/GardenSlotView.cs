using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Growing
{
    public class GardenSlotView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _ammoAmount;
        [SerializeField] private Image _fillImage;
        [SerializeField] private Button _button;

        [Header("Fill Animation")] 
        [SerializeField] private float _fillSpeed = 1;
        
        private float _targetFillValue;

        public void Initialize(GardenSlotViewPresenter presenter)
        {
            presenter.Icon.Subscribe((icon) =>
            {
                _icon.sprite = icon;
                _icon.color = icon == null ? Color.clear : Color.white;
            }).AddTo(this);
            
            presenter.FillColor.Subscribe((color) =>
            {
                _fillImage.color = color;
            }).AddTo(this);
            
            presenter.FillPart.Subscribe((part) =>
            {
                _targetFillValue = part;

                if (_targetFillValue == 1 || _targetFillValue == 0)
                    _fillImage.fillAmount = _targetFillValue;
            }).AddTo(this);
            
            presenter.AmmoAmount.Subscribe((ammo) =>
            {
                _ammoAmount.text = ammo;
            }).AddTo(this);

            presenter.ChooseAction.BindTo(_button).AddTo(this);
        }

        private void Update()
        {
            if (_targetFillValue != _fillImage.fillAmount)
            {
                _fillImage.fillAmount = Mathf.MoveTowards(_fillImage.fillAmount, _targetFillValue, Time.deltaTime * _fillSpeed);
            }
        }
    }
}