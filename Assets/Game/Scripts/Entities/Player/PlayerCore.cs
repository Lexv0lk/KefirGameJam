using System;
using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Components;
using Game.Scripts.Fabrics;
using Game.Scripts.Models;

namespace Game.Scripts.Entities
{
    [Serializable]
    public class PlayerCore
    {
        public SimpleMoveComponent MoveComponent;
        public RotateComponent RotateComponent;
        public LifeComponent LifeComponent;
        public ShootComponent ShootComponent;
        
        public void Compose(IBulletFabric bulletFabric, RiffleStoreModel riffleStoreModel)
        {
            LifeComponent.Compose();
            MoveComponent.Compose();
            RotateComponent.Compose();
            ShootComponent.Compose(bulletFabric, riffleStoreModel);

            AtomicFunction<bool> isAlive = new AtomicFunction<bool>(IsAlive);
            
            MoveComponent.CanMove.Append(isAlive);
            RotateComponent.CanRotate.Append(isAlive);
            ShootComponent.CanShoot.Append(isAlive);
        }
        
        public IEnumerable<IAtomicLogic> GetMechanics()
        {
            List<IAtomicLogic> mechanics = new();
            
            mechanics.AddRange(MoveComponent.GetMechanics());
            mechanics.AddRange(RotateComponent.GetMechanics());
            mechanics.AddRange(LifeComponent.GetMechanics());
            mechanics.AddRange(ShootComponent.GetMechanics());

            return mechanics;
        }

        private bool IsAlive()
        {
            return LifeComponent.IsDead.Value == false;
        }
    }
}