using Game.Scripts.Growing;
using Game.Scripts.UI.Presenters;
using UnityEngine;

namespace Game.Scripts.UI.Views
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private SeedSlotView[] _seedSlots;

        public void Initialize(InventoryViewPresenter presenter)
        {
            for (int i = 0; i < presenter.SeedSlotViewPresenters.Count; i++)
                _seedSlots[i].Initialize(presenter.SeedSlotViewPresenters[i]);
        }
    }
}