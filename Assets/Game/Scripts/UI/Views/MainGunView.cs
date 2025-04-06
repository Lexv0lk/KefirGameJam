using Game.Scripts.UI.Presenters;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Views
{
    public class MainGunView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _ammoAmount;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _fill;
        [Space]
        [SerializeField] private float _fillSpeed = 1f;
        
        private float _targetFillAmount;

        public void Initialize(MainGunViewPresenter presenter)
        {
            presenter.Icon.Subscribe((ico) =>
            {
                _icon.sprite = ico;
                _icon.color = ico == null ? Color.clear : Color.white;
            }).AddTo(this);
            
            presenter.FillColor.Subscribe((color) =>
            {
                _fill.color = color;
            }).AddTo(this);
            
            presenter.AmmoLeftPart.Subscribe((part) =>
            {
                _targetFillAmount = part;
            }).AddTo(this);
            
            presenter.AmmoLeftText.Subscribe((text) =>
            {
                _ammoAmount.text = text;
            }).AddTo(this);
        }
        
        private void Update()
        {
            if (_fill.fillAmount != _targetFillAmount)
                _fill.fillAmount = Mathf.MoveTowards(_fill.fillAmount, _targetFillAmount, Time.deltaTime * _fillSpeed);
        }
    }
}