using Game.Scripts.Controllers;
using Game.Scripts.Loot;
using UniRx;
using UnityEngine;

namespace Game.Scripts.Growing
{
    public class GardenSlotViewPresenter
    {
        private readonly GardenController _gardenController;
        private readonly GardenViewConfig _config;
        private readonly WeaponChangeController _weaponChangeController;
        private readonly CompositeDisposable _compositeDisposable = new();

        private GrowInfo _currentInfo;
        private SeedConfig _lastSeedConfig;
        private CompositeDisposable _growDisposable = new();

        public BoolReactiveProperty IsClickable { get; } = new();
        public ReactiveProperty<Sprite> Icon { get; } = new();
        public ReactiveProperty<Color> FillColor { get; } = new();
        public FloatReactiveProperty FillPart { get; } = new();
        public StringReactiveProperty AmmoAmount { get; } = new();
        
        public ReactiveCommand ChooseRequest { get; } = new();
        public ReactiveCommand ChooseStopRequest { get; } = new();
        public ReactiveCommand ChooseAction { get; }
        public ReactiveCommand<GardenSlotViewPresenter> ChooseEvent { get; }

        public ReactiveCommand<SeedConfig> PlantRequest { get; } = new();
        public ReactiveCommand DeleteRequest { get; } = new();
        public ReactiveCommand WaterRequest { get; } = new();
        public ReactiveCommand CollectRequest { get; } = new();

        public GardenSlotViewPresenter(GardenController gardenController, GardenViewConfig config, WeaponChangeController weaponChangeController)
        {
            _gardenController = gardenController;
            _config = config;
            _weaponChangeController = weaponChangeController;

            ChooseAction = new ReactiveCommand(IsClickable);

            ChooseRequest.Subscribe(OnChooseRequested).AddTo(_compositeDisposable);
            ChooseStopRequest.Subscribe(OnChooseStopRequested).AddTo(_compositeDisposable);
            ChooseAction.Subscribe(OnChooseActed).AddTo(_compositeDisposable);

            PlantRequest.Subscribe(OnPlantRequested).AddTo(_compositeDisposable);
            DeleteRequest.Subscribe(OnPlantDeleteRequested).AddTo(_compositeDisposable);
            WaterRequest.Subscribe(OnPlantWaterRequested).AddTo(_compositeDisposable);
            CollectRequest.Subscribe(OnPlantCollectRequested).AddTo(_compositeDisposable);
            
            _gardenController.Died += OnPlantDied;
        }

        private void OnPlantDied(GrowInfo growInfo)
        {
            if (growInfo != _currentInfo)
                return;
            
            _growDisposable.Dispose();
            _growDisposable = new();

            Icon.Value = null;
            FillPart.Value = 0;
            AmmoAmount.Value = string.Empty;
        }

        private void OnPlantRequested(SeedConfig seed)
        {
            _currentInfo = _gardenController.Plant(seed);
            _growDisposable = new();

            Icon.Value = _config.GrowIcon;
            FillColor.Value = _config.GrowingColor;
            FillPart.Value = 0;

            _currentInfo.GrowTimeLeft.Subscribe(OnGrowing).AddTo(_growDisposable);
            _currentInfo.IsGrowed.Subscribe(OnGrowed).AddTo(_growDisposable);
        }

        private void OnPlantDeleteRequested(Unit _)
        {
            if (_currentInfo == null)
                return;
            
            _gardenController.Remove(_currentInfo);
        }

        private void OnPlantWaterRequested(Unit _)
        {
            if (_currentInfo == null)
                return;
            
            _gardenController.Water(_currentInfo);
        }

        private void OnPlantCollectRequested(Unit _)
        {
            if (_currentInfo == null && _currentInfo.IsGrowed.Value == false)
                return;
            
            _weaponChangeController.Change(_currentInfo.Result, Mathf.CeilToInt(_currentInfo.AmmoCountAccumulated.Value));
            _gardenController.Collect(_currentInfo);
        }

        #region GROW

        private void OnGrowing(float timeLeft)
        {
            FillPart.Value = (_currentInfo.GrowData.GrowTime - timeLeft) / _currentInfo.GrowData.GrowTime;
        }
        
        private void OnGrowed(bool isGrowed)
        {
            if (isGrowed == false)
                return;
            
            _growDisposable.Dispose();
            _growDisposable = new();

            Icon.Value = _currentInfo.Result.Metadata.Icon;
            FillColor.Value = _config.MaturationColor;
            FillPart.Value = 1;
            
            _currentInfo.MaturationTimeLeft.Subscribe(OnMaturationTimeChanged).AddTo(_growDisposable);
            _currentInfo.AmmoCountAccumulated.Subscribe(OnAmmoChanged).AddTo(_growDisposable);
        }

        private void OnAmmoChanged(float ammoAmount)
        {
            int fullAmmos = Mathf.CeilToInt(ammoAmount);
            AmmoAmount.Value = $"{fullAmmos}/{_currentInfo.Result.AmmoAmount}";
        }

        private void OnMaturationTimeChanged(float timeLeft)
        {
            FillPart.Value = timeLeft / _currentInfo.GrowData.MaturationTime;
        }

        #endregion

        #region CHOOSE

        private void OnChooseStopRequested(Unit obj)
        {
            IsClickable.Value = false;
        }

        private void OnChooseActed(Unit _)
        {
            ChooseEvent.Execute(this);
        }

        private void OnChooseRequested(Unit _)
        {
            if (_currentInfo != null)
                return;

            IsClickable.Value = true;
        }

        #endregion

        ~GardenSlotViewPresenter()
        {
            if (_growDisposable != null)
                _growDisposable.Dispose();
            
            _compositeDisposable.Dispose();
            _gardenController.Died -= OnPlantDied;
        }
    }
}