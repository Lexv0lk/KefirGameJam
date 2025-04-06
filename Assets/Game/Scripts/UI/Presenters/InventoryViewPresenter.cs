using Game.Scripts.Growing;
using Game.Scripts.Loot;
using UniRx;
using Zenject;

namespace Game.Scripts.UI.Presenters
{
    public class InventoryViewPresenter
    {
        public ReactiveCollection<SeedSlotViewPresenter> SeedSlotViewPresenters { get; } = new();
        
        public InventoryViewPresenter(SeedsCatalog seedsCatalog, DiContainer diContainer)
        {
            foreach (var seed in seedsCatalog.AllItems)
            {
                var presenterInstance = diContainer.Instantiate<SeedSlotViewPresenter>(new object[] { seed });
                SeedSlotViewPresenters.Add(presenterInstance);
            }
        }
    }
}