using Game.Scripts.Configs.Models;
using Game.Scripts.Controllers;
using Game.Scripts.Models;
using UniRx;
using UnityEngine;

namespace Game.Scripts.UI.Presenters
{
    public class MainGunViewPresenter
    {
        private readonly WeaponChangeController _weaponChangeController;
        private readonly RiffleStoreModel _ammoModel;
        private readonly CompositeDisposable _disposable = new();

        public ReactiveProperty<Sprite> Icon { get; } = new();
        public FloatReactiveProperty AmmoLeftPart { get; } = new();
        public ReactiveProperty<Color> FillColor { get; } = new();
        public StringReactiveProperty AmmoLeftText { get; } = new();

        public MainGunViewPresenter(WeaponChangeController weaponChangeController, RiffleStoreModel ammoModel)
        {
            _weaponChangeController = weaponChangeController;
            _ammoModel = ammoModel;

            ammoModel.AmmunitionAmount.Subscribe(UpdateAmmoAmount).AddTo(_disposable);
            ammoModel.MaxAmmunitionAmount.Subscribe(UpdateAmmoAmount).AddTo(_disposable);

            OnWeaponChanged(weaponChangeController.CurrentWeapon);
            
            _weaponChangeController.Changed += OnWeaponChanged;
        }

        private void UpdateAmmoAmount(int _)
        {
            AmmoLeftPart.Value = _ammoModel.AmmunitionAmount.Value / (float)_ammoModel.MaxAmmunitionAmount.Value;
            AmmoLeftText.Value = $"{_ammoModel.AmmunitionAmount.Value}/{_ammoModel.MaxAmmunitionAmount.Value}";
        }

        private void OnWeaponChanged(WeaponConfig newWeapon)
        {
            Icon.Value = newWeapon.Metadata.Icon;
            FillColor.Value = newWeapon.Metadata.FillColor;
        }
        
        ~MainGunViewPresenter()
        {
            _disposable.Dispose();
            _weaponChangeController.Changed -= OnWeaponChanged;
        }
    }
}