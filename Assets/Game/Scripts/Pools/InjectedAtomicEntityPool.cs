using Atomic.Objects;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Pools
{
    public class InjectedAtomicEntityPool : AtomicEntityPool
    {
        private DiContainer _container;
        
        [Inject]
        private void Construct(DiContainer container)
        {
            _container = container;
        }

        protected override AtomicEntity Instantiate(AtomicEntity prefab, Transform root)
        {
            return _container.InstantiatePrefab(prefab, root).GetComponent<AtomicEntity>();
        }
    }
}