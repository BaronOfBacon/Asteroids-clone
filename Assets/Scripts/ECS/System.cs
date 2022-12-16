using System;
using System.Collections.Generic;

namespace ECS
{
    /// <summary>
    /// ECS system
    /// </summary>
    /// <typeparam name="Type">Type must be inherited from ECS.component</typeparam>
    public abstract class System
    {
        public abstract IEnumerable<Type> ComponentsMask { get;}

        public abstract void Process(Entity entity);
        public abstract void PostProcess();
        public abstract void Destroy();
    }
    
    public interface IUpdatableSystem
    {
        protected virtual void Update(){}
    }
    
}
