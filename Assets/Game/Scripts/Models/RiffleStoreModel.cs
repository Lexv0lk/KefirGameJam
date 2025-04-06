using Atomic.Elements;
using Game.Scripts.Configs.Models;
using UniRx;

namespace Game.Scripts.Models
{
    public class RiffleStoreModel
    {
        public ReactiveProperty<int> AmmunitionAmount { get; }
        public ReactiveProperty<int> MaxAmmunitionAmount { get; }

        public RiffleStoreModel(RiffleStoreConfig config)
        {
            AmmunitionAmount = new ReactiveProperty<int>(config.StartAmount);
            MaxAmmunitionAmount = new ReactiveProperty<int>(config.MaximalAmount);
        }
    }
}