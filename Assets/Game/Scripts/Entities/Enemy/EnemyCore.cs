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
        public SimpleMoveComponent MoveComponent;
        public RotateComponent RotateComponent;
        public LifeComponent LifeComponent;
        
        [SerializeField] private float _followReachDistance = 1;
        [SerializeField] private Collider _collider;
        
        private FollowTargetMechanic _followTargetMechanic;
        private LookAtTargetMechanic _lookAtTargetMechanic;
        private ColliderStateChangeFromDeathMechanic _colliderStateMechanic;
        private IAtomicValue<AtomicEntity> _target;
        
        private List<IAtomicLogic> _mechanics = new();
                
        public void Compose(IAtomicValue<Vector3> rootPosition, IAtomicValue<AtomicEntity> target)
        {
            _target = target;
            
            AtomicFunction<bool> isTargetAlive = new AtomicFunction<bool>(IsTargetAlive);
            AtomicFunction<bool> isAlive = new AtomicFunction<bool>(IsAlive);
            AtomicFunction<bool> isNotInAttack = new AtomicFunction<bool>(IsNotInAttack);
            
            LifeComponent.Compose();
            MoveComponent.Compose();
            RotateComponent.Compose();

            _followTargetMechanic = new FollowTargetMechanic(_target,
                rootPosition, MoveComponent.Direction, _followReachDistance);
            
            _lookAtTargetMechanic = new LookAtTargetMechanic(_target,
                rootPosition, RotateComponent.ForwardDirection);
            
            _colliderStateMechanic =
                new ColliderStateChangeFromDeathMechanic(LifeComponent.IsDead, _collider);
            
            AttackComponent.Compose(_followTargetMechanic.IsReachedTarget, target);
            
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