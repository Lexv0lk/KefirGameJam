using Game.Scripts.Growing;
using UniRx;
using UnityEngine;

namespace Game.Scripts.UI.Presenters
{
    public class GrowStatusViewPresenter
    {
        private readonly int _index;
        private readonly GardenViewConfig _gardenViewConfig;
        private readonly CompositeDisposable _compositeDisposable = new();
        
        public ReactiveProperty<Sprite> Icon { get; } = new();
        public ReactiveProperty<Color> FillColor { get; } = new();
        public FloatReactiveProperty FillPart { get; } = new();

        private GrowInfo _currentGrowInfo;
        private CompositeDisposable _growDisposable;
        
        public GrowStatusViewPresenter(int index, GardenController gardenController, GardenViewConfig gardenViewConfig)
        {
            _index = index;
            _gardenViewConfig = gardenViewConfig;
            
            if (gardenController.CurrentGrowings.Count > index)
                SetInfoUpdating(gardenController.CurrentGrowings[index]);

            gardenController.CurrentGrowings.ObserveAdd().Subscribe(OnAddedGrowing).AddTo(_compositeDisposable);
            gardenController.CurrentGrowings.ObserveRemove().Subscribe(OnRemovedGrowing).AddTo(_compositeDisposable);
        }

        private void OnRemovedGrowing(CollectionRemoveEvent<GrowInfo> context)
        {
            if (context.Index == _index)
                RemoveLastInfoUpdating();
        }

        private void OnAddedGrowing(CollectionAddEvent<GrowInfo> context)
        {
            if (context.Index == _index)
                SetInfoUpdating(context.Value);
        }

        private void RemoveLastInfoUpdating()
        {
            if (_growDisposable != null && _growDisposable.IsDisposed == false)
                _growDisposable.Dispose();

            _currentGrowInfo = null;
            Icon.Value = null;
            FillPart.Value = 0;
        }

        private void SetInfoUpdating(GrowInfo newInfo)
        {
            _currentGrowInfo = newInfo;
            _growDisposable = new();

            newInfo.IsGrowed.Subscribe((isGrowed) =>
            {
                Icon.Value = !isGrowed ? _gardenViewConfig.GrowIcon : _currentGrowInfo.Result.Metadata.Icon;
                FillColor.Value = !isGrowed ? _gardenViewConfig.GrowingColor : _gardenViewConfig.MaturationColor;
                
                if (newInfo.IsGrowed.Value)
                {
                    newInfo.MaturationTimeLeft.Subscribe((timeLeft) =>
                    {
                        FillPart.Value = timeLeft / _currentGrowInfo.GrowData.MaturationTime;
                    }).AddTo(_growDisposable);
                }
                else
                {
                    newInfo.GrowTimeLeft.Subscribe((timeLeft) =>
                    {
                        FillPart.Value = (_currentGrowInfo.GrowData.GrowTime - timeLeft) / _currentGrowInfo.GrowData.GrowTime;
                    }).AddTo(_growDisposable);
                }
            }).AddTo(_growDisposable);
        }
        
        ~GrowStatusViewPresenter()
        {
            RemoveLastInfoUpdating();
            _compositeDisposable.Dispose();
        }
    }
}