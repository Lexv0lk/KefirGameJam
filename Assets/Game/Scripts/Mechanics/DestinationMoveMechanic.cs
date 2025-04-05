using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Scripts.Mechanics
{
    public class DestinationMoveMechanic : IAtomicEnable, IAtomicDisable, IAtomicUpdate
    {
        private readonly IAtomicValueObservable<Vector3> _destination;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly IAtomicValue<bool> _canMove;

        public DestinationMoveMechanic(IAtomicValueObservable<Vector3> destination, NavMeshAgent navMeshAgent,
            IAtomicValue<bool> canMove)
        {
            _destination = destination;
            _navMeshAgent = navMeshAgent;
            _canMove = canMove;
        }
        
        public void Enable()
        {
            SetDestination(_destination.Value);
            _destination.Subscribe(SetDestination);
        }

        public void Disable()
        {
            _destination.Unsubscribe(SetDestination);
        }
        
        private void SetDestination(Vector3 destination)
        {
            if (_canMove.Value)
                _navMeshAgent.SetDestination(destination);
        }

        public void OnUpdate(float deltaTime)
        {
            _navMeshAgent.isStopped = !_canMove.Value;
        }
    }
}