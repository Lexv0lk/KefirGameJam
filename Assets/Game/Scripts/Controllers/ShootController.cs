using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Configs.Input;
using Game.Scripts.Models;
using Game.Scripts.Tech;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Controllers
{
    public class ShootController : ITickable
    {
        private readonly InputConfig _config;
        private readonly InputModel _inputModel;
        private readonly IAtomicAction _shootRequest;

        private bool _enabled;

        public ShootController(IAtomicEntity entity, InputConfig config, InputModel inputModel)
        {
            _config = config;
            _inputModel = inputModel;
            _shootRequest = entity.Get<IAtomicAction>(ShootAPI.SHOOT_REQUEST);
            _enabled = true;
        }
        
        public void Tick()
        {
            if (_enabled == false || _inputModel.IsPlayerInputEnabled == false)
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