using System;
using System.Collections.Generic;

namespace ECS
{
    public abstract class System
    {
        public IEnumerable<Type> ComponentsMask { get; }
    }
    
    public interface IUpdatableSystem
    {
        protected virtual void Update(){}
    }
    
}
