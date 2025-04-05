using System;

namespace Game.Scripts.Components
{
    [Serializable]
    public abstract class Component
    {
        public virtual void Compose() {}
        
        public virtual void Update(float deltaTime) {}
        
        public virtual void Dispose() {}
    }
}