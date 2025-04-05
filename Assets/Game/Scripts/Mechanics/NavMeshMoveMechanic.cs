using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Scripts.Mechanics
{
    public class NavMeshMoveMechanic : IAtomicUpdate
    {
        private readonly IAtomicValue<bool> _canMove;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly IAtomicValue<Vector3> _direction;
        private readonly IAtomicValue<float> _speed;

        public NavMeshMoveMechanic(IAtomicValue<bool> canMove, NavMeshAgent navMeshAgent,
            IAtomicValue<Vector3> direction, IAtomicValue<float> speed)
        {
            _canMove = canMove;
            _navMeshAgent = navMeshAgent;
            _direction = direction;
            _speed = speed;
        }
        
        public void OnUpdate(float deltaTime)
        {
            if (_canMove.Value == false)
            {
                _navMeshAgent.velocity = Vector3.zero;
                return;
            }

            _navMeshAgent.velocity = _direction.Value.normalized * _speed.Value;
        }
    }
}