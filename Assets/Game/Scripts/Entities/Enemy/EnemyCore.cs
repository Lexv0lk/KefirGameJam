using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Components;
using Game.Scripts.Mechanics;
using Game.Scripts.Tech;
using UnityEngine;

namespace Game.Scripts.Entities
{
    [Serializable]
    public class EnemyCore
    {
        public AttackComponent AttackComponent;
        public TargetMoveComponent MoveComponent;
        public RotateComponent RotateComponent;
        public LifeComponent LifeComponent;
        
        [SerializeField] private Collider _collider;
        
        private FollowTargetMechanic _followTargetMechanic;
        private LookAtTargetMechanic _lookAtTargetMechanic;
        private ColliderStateChangeFromDeathMechanic _colliderStateMechanic;
        private IAtomicValueObservable<AtomicEntity> _target;
        private TargetReachCalculationMechanic _targetReachCalculationMechanic;
        
        private List<IAtomicLogic> _mechanics = new();
                
        public void Compose(IAtomicValue<Vector3> rootPosition, IAtomicValueObservable<AtomicEntity> target)
        {
            _target = target;
            
            AtomicFunction<bool> isTargetAlive = new AtomicFunction<bool>(IsTargetAlive);
            AtomicFunction<bool> isAlive = new AtomicFunction<bool>(IsAlive);
            AtomicFunction<bool> isNotInAttack = new AtomicFunction<bool>(IsNotInAttack);
            
            LifeComponent.Compose();
            MoveComponent.Compose();
            RotateComponent.Compose();

            _followTargetMechanic = new FollowTargetMechanic(MoveComponent.Destination,
                _target, AttackComponent.AttackRange, MoveComponent.StoppingDistance);
            
            _lookAtTargetMechanic = new LookAtTargetMechanic(_target,
                rootPosition, RotateComponent.ForwardDirection);
            
            _colliderStateMechanic =
                new ColliderStateChangeFromDeathMechanic(LifeComponent.IsDead, _collider);

            _targetReachCalculationMechanic =
                new TargetReachCalculationMechanic(MoveComponent.DistanceToDestination, AttackComponent.AttackRange);
            
            AttackComponent.Compose(_targetReachCalculationMechanic.IsTargetReached, target);
            
            MoveComponent.CanMove.Append(isNotInAttack);
            
            MoveComponent.CanMove.Append(isAlive);
            RotateComponent.CanRotate.Append(isAlive);
            AttackComponent.CanAttack.Append(isAlive);
            
            MoveComponent.CanMove.Append(isTargetAlive);
            RotateComponent.CanRotate.Append(isTargetAlive);
            AttackComponent.CanAttack.Append(isTargetAlive);
            
            _mechanics.AddRange(MoveComponent.GetMechanics());
            _mechanics.AddRange(RotateComponent.GetMechanics());
            _mechanics.AddRange(LifeComponent.GetMechanics());
            _mechanics.AddRange(AttackComponent.GetMechanics());
            _mechanics.Add(_followTargetMechanic);
            _mechanics.Add(_lookAtTargetMechanic);
            _mechanics.Add(_colliderStateMechanic);
            _mechanics.Add(_targetReachCalculationMechanic);
        }

        public IEnumerable<IAtomicLogic> GetMechanics() => _mechanics;

        private Vector3 GetTargetPosition()
        {
            return _target.Value.transform.position;
        }
        
        private bool IsAlive()
        {
            return LifeComponent.IsDead.Value == false;
        }
        
        private bool IsTargetAlive()
        {
            return _target.Value != null && _target.Value.Get<IAtomicValue<bool>>(LifeAPI.IS_DEAD).Value == false;
        }
        
        private bool IsNotInAttack()
        {
            return !AttackComponent.IsInAttack.Value;
        }
    }
}