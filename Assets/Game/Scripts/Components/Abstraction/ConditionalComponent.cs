using System;
using Atomic.Elements;
using Game.Scripts.Utilities;

namespace Game.Scripts.Components
{
    [Serializable]
    public abstract class ConditionalComponent : Component
    {
        protected readonly AtomicAnd Condition = new();
    }
}