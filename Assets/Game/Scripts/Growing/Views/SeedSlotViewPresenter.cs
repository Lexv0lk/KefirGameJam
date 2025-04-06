using Game.Scripts.Loot;
using UniRx;
using UnityEngine;

namespace Game.Scripts.Growing
{
    public class SeedSlotViewPresenter
    {
        private readonly SeedConfig _seedConfig;
        private readonly Inventory.Inventory _inventory;
        private readonly GardenController _gardenController;
        private readonly CompositeDisposable _compositeDisposable = new();

        private BoolReactiveProperty _canUse;
        
        public Sprite Icon { get; }
        public StringReactiveProperty Count { get; } = new();
        public ReactiveCommand UseCommand { get; }
        
        public ReactiveCommand<SeedConfig> UseEvent { get; } = new();

        public SeedSlotViewPresenter(SeedConfig seedConfig, Inventory.Inventory inventory,
            GardenController gardenController)
        {
            _seedConfig = seedConfig;
            _inventory = inventory;
            _gardenController = gardenController;
            
            Icon = seedConfig.Metadata.Icon;
            Count.Value = _inventory.GetCount(_seedConfig).ToString();
            
            _canUse = new BoolReactiveProperty(gardenController.CanPlant(_seedConfig));
            UseCommand = new ReactiveCommand(_canUse);

            UseCommand.Subscribe(OnUseCommand).AddTo(_compositeDisposable);
            
            _inventory.Changed += OnInventoryChanged;
        }

        private void OnUseCommand(Unit _)
        {
            UseEvent.Execute(_seedConfig);
        }

        private void OnInventoryChanged()
        {
            _canUse.Value = _gardenController.CanPlant(_seedConfig);
            Count.Value = _inventory.GetCount(_seedConfig).ToString();
        }
        
        ~SeedSlotViewPresenter()
        {
            _inventory.Changed -= OnInventoryChanged;
            _compositeDisposable.Dispose();
        }
    }
}