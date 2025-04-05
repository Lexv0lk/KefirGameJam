using System;
using Atomic.Objects;

namespace Game.Scripts.Pools
{
    public interface IAtomicEntityPool
    {
        event Action<AtomicEntity> Given;
        event Action<AtomicEntity> Released;
        AtomicEntity GetEntity();
        void ReleaseEntity(AtomicEntity entity);
    }
}