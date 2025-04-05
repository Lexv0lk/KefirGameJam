using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Configs.Input;
using Game.Scripts.Tech;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Controllers
{
    public class InputMoveController : ITickable 
    {
        private readonly IAtomicVariable<Vector3> _moveDirection;
        private readonly InputConfig _config;
        
        private Vector3 _cachedDirection;

        public InputMoveController(IAtomicEntity entity, InputConfig config)
        {
            _config = config;
            _moveDirection = entity.Get<IAtomicVariable<Vector3>>(MoveAPI.MOVE_DIRECTION);
        }

        public void Tick()
        {
            _cachedDirection = Vector3.zero;
            
            if (Input.GetKey(_config.Up))
                _cachedDirection.z += 1;

            if (Input.GetKey(_config.Down))
                _cachedDirection.z += -1;
            
            if (Input.GetKey(_config.Left))
                _cachedDirection.x += -1;
            
            if (Input.GetKey(_config.Right))
                _cachedDirection.x += 1;
            
            Move(_cachedDirection);
        }

        private void Move(Vector3 direction)
        {
            _moveDirection.Value = direction;
        }
    }
}