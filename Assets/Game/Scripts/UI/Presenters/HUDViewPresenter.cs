using Game.Scripts.Growing;
using UniRx;
using Zenject;

namespace Game.Scripts.UI.Presenters
{
    public class HUDViewPresenter
    {
        private readonly GardenControlConfig _gardenControlConfig;
        private readonly Inventory.Inventory _inventory;

        public ReactiveCollection<GrowStatusViewPresenter> GrowStatusViewPresenters { get; } = new();
        public MainGunViewPresenter MainGunViewPresenter { get; }

        public FloatReactiveProperty WaterFillAmount { get; } = new();
        
        public HUDViewPresenter(DiContainer diContainer, GardenControlConfig gardenControlConfig, Inventory.Inventory inventory)
        {
            _gardenControlConfig = gardenControlConfig;
            _inventory = inventory;

            for (int i = 0; i < _gardenControlConfig.GardenPlaceCount; i++)
            {
                var presenterInstance = diContainer.Instantiate<GrowStatusViewPresenter>(new object[] { i });
                GrowStatusViewPresenters.Add(presenterInstance);
            }

            MainGunViewPresenter = diContainer.Instantiate<MainGunViewPresenter>();
            
            _inventory.Changed += OnInventoryChanged;
        }

        private void OnInventoryChanged()
        {
            WaterFillAmount.Value = (float)_inventory.GetCount(_gardenControlConfig.WaterLootConfig) /
                                    _inventory.GetMaxCount(_gardenControlConfig.WaterLootConfig);
        }

        ~HUDViewPresenter()
        {
            _inventory.Changed -= OnInventoryChanged;
        }
    }
}