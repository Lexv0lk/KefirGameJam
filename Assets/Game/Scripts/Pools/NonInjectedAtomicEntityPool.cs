using Atomic.Objects;
using UnityEngine;

namespace Game.Scripts.Pools
{
    public class NonInjectedAtomicEntityPool : AtomicEntityPool
    {
        protected override AtomicEntity Instantiate(AtomicEntity prefab, Transform root)
        {
            return GameObject.Instantiate(prefab, root);
        }
    }
}