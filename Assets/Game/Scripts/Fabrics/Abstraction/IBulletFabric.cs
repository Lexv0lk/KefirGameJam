using System;
using Atomic.Objects;

namespace Game.Scripts.Fabrics
{
    public interface IBulletFabric
    {
        AtomicEntity GetBullet(AtomicEntity prefab);
    }
}