using Atomic.Objects;
using Game.Scripts.Models;
using Game.Scripts.UI.Presenters;
using Game.Scripts.UI.Views;

namespace Game.Scripts.UI.Controllers
{
    public class GameInfoViewAdapter
    {
        public GameInfoViewAdapter(GameInfoView view, RiffleStoreModel ammoModel, KillCountModel killsModel,
            AtomicEntity player)
        {
            IGameInfoViewPresenter presenter = new GameInfoViewPresenter(ammoModel, killsModel, player);
            view.Compose(presenter);
        }
    }
}