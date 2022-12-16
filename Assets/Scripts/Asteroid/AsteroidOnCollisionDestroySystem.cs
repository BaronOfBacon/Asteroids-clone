using System;
using System.Collections.Generic;
using Asteroids.Collisions;
using ECS;

namespace Asteroids.Asteroid
{
    public class AsteroidOnCollisionDestroySystem : ECS.System
    {
        public override IEnumerable<Type> ComponentsMask => _componentsMask;

        private readonly IEnumerable<Type> _componentsMask;

        public AsteroidOnCollisionDestroySystem()
        {
            _componentsMask = new List<Type>()
            {
                typeof(AsteroidComponent),
                typeof(TriggerDetectorComponent)
            };
        }
        
        public override void Process(Entity entity)
        {
            var triggerDetector = entity.GetComponent<TriggerDetectorComponent>();
            if(triggerDetector.CollidingObjects.Count > 0)
                entity.InitDestroy();
        }

        public override void PostProcess()
        {
        }

        public override void Destroy()
        {
        }
    }
}
