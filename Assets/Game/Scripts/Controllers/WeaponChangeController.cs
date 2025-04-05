using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Configs;
using Game.Scripts.Configs.Models;
using Game.Scripts.Models;
using Game.Scripts.Tech;
using Zenject;

namespace Game.Scripts.Controllers
{
    public class WeaponChangeController : IInitializable
    {
        private readonly AtomicEntity _player;
        private readonly RiffleStoreModel _riffleStoreModel;
        private readonly WeaponChangeConfig _config;

        public WeaponChangeController(AtomicEntity player, RiffleStoreModel riffleStoreModel, WeaponChangeConfig config)
        {
            _player = player;
            _riffleStoreModel = riffleStoreModel;
            _config = config;
        }
        
        public void Initialize()
        {
            Change(_config.StartWeapon);
        }

        public void Change(WeaponConfig newWeapon)
        {
            _player.Get<IAtomicVariable<WeaponConfig>>(ShootAPI.CURRENT_WEAPON).Value = newWeapon;
            _riffleStoreModel.MaxAmmunitionAmount.Value = newWeapon.AmmoAmount;
            _riffleStoreModel.AmmunitionAmount.Value = _riffleStoreModel.MaxAmmunitionAmount.Value;
        }
    }
}