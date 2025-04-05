using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Configs.Input;
using Game.Scripts.Tech;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Controllers
{
    public class ShootController : ITickable
    {
        private readonly InputConfig _config;
        private readonly IAtomicAction _shootRequest;

        private bool _enabled;

        public ShootController(IAtomicEntity entity, InputConfig config)
        {
            _config = config;
            _shootRequest = entity.Get<IAtomicAction>(ShootAPI.SHOOT_REQUEST);
            _enabled = true;
        }
        
        public void Tick()
        {
            if (_enabled == false)
                return;
            
            if (Input.GetMouseButton((int)_config.Shoot))
                _shootRequest.Invoke();
        }

        public void Disable()
        {
            _enabled = false;
        }
    }
}