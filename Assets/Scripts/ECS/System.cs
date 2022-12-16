using System;
using System.Collections.Generic;
using ECS.Messages;

namespace ECS
{
    /// <summary>
    /// ECS system
    /// </summary>
    /// <typeparam name="Type">Type must be inherited from ECS.component</typeparam>
    public abstract class System
    {
        public abstract IEnumerable<Type> ComponentsMask { get;}
        protected Dispatcher MessageDispatcher { get; set; }
        protected World World { get; set; }
        private bool _isInitialized;

        public virtual void Initialize(World world, Dispatcher messageDispatcher)
        {
            if (_isInitialized) return;
            World = world;
            MessageDispatcher = messageDispatcher;
        }

        public abstract void Process(Entity entity);
        public abstract void PostProcess();
        public abstract void Destroy();
    }
    
    public interface IUpdatableSystem
    {
        protected virtual void Update(){}
    }
    
}
