using System;
using Game.Scripts.Loot;
using UniRx;
using Zenject;

namespace Game.Scripts.Growing
{
    public class GardenViewPresenter
    {
        private readonly SeedsCatalog _seedsCatalog;
        private readonly DiContainer _diContainer;
        private readonly CompositeDisposable _compositeDisposable = new();
        
        private CompositeDisposable _chooseDisposable = new();
        
        public ReactiveCollection<SeedSlotViewPresenter> SeedSlots { get; } = new();
        public ReactiveCollection<GardenSlotViewPresenter> GardenSlots { get; } = new();

        public ReactiveCommand WaterRequest { get; } = new();
        public ReactiveCommand RemoveRequest { get; } = new();
        public ReactiveCommand CollectRequest { get; } = new();

        public ReactiveCommand ExitEvent { get; } = new();
        
        public GardenViewPresenter(SeedsCatalog seedsCatalog, DiContainer diContainer, GardenControlConfig gardenControlConfig)
        {
            _seedsCatalog = seedsCatalog;
            _diContainer = diContainer;

            foreach (var seed in _seedsCatalog.AllItems)
            {
                var presenterInstance = _diContainer.Instantiate<SeedSlotViewPresenter>(new object[] { seed });
                SeedSlots.Add(presenterInstance);

                presenterInstance.UseEvent.Subscribe(OnSeedUsed).AddTo(_compositeDisposable);
            }

            for (int i = 0; i < gardenControlConfig.GardenPlaceCount; i++)
            {
                var presenterInstance = _diContainer.Instantiate<GardenSlotViewPresenter>();
                GardenSlots.Add(presenterInstance);
            }
            
            WaterRequest.Subscribe(OnWaterRequested).AddTo(_compositeDisposable);
            RemoveRequest.Subscribe(OnRemoveRequested).AddTo(_compositeDisposable);
            CollectRequest.Subscribe(OnCollectRequested).AddTo(_compositeDisposable);
            ExitEvent.Subscribe(StopChoosing).AddTo(_compositeDisposable);
        }

        private void OnWaterRequested(Unit _)
        {
            HandleGardenAction(slot => slot.WaterRequest.Execute());
        }

        private void OnRemoveRequested(Unit _)
        {
            HandleGardenAction(slot => slot.DeleteRequest.Execute());
        }

        private void OnCollectRequested(Unit _)
        {
            HandleGardenAction(slot => slot.CollectRequest.Execute());
        }

        private void OnSeedUsed(SeedConfig seed)
        {
            HandleGardenAction(slot => slot.PlantRequest.Execute(seed));
        }
        
        private void HandleGardenAction(Action<GardenSlotViewPresenter> onSlotSelected)
        {
            StopChoosing(default);
            _chooseDisposable = new();

            foreach (var slot in GardenSlots)
            {
                slot.ChooseRequest.Execute();
                slot.ChooseEvent.Subscribe(s =>
                {
                    StopChoosing(default);
                    onSlotSelected(s);
                    _chooseDisposable.Dispose();
                }).AddTo(_chooseDisposable);
            }
        }

        private void StopChoosing(Unit _)
        {
            foreach (var slot in GardenSlots)
                slot.ChooseStopRequest.Execute();
            
            if (_chooseDisposable != null && _chooseDisposable.IsDisposed == false)
                _chooseDisposable.Dispose();
        }
        
        ~GardenViewPresenter()
        {
            _compositeDisposable.Dispose();

            if (_chooseDisposable != null && _chooseDisposable.IsDisposed == false)
                _chooseDisposable.Dispose();
        }
    }
}