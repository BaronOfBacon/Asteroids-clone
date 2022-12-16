using System;
using System.Collections.Generic;
using Asteroids.Collisions;
using Asteroids.Movable;
using ECS;

namespace Asteroids.Weapon.Projectile
{
    public class ProjectileOnCollisionDestroySystem : ECS.System
    {
        public override IEnumerable<Type> ComponentsMask => _componentsMask;
        
        private readonly IEnumerable<Type> _componentsMask;

        public ProjectileOnCollisionDestroySystem()
        {
            _componentsMask = new List<Type>{
                typeof(ProjectileComponent),
                typeof(TriggerDetectorComponent)
            };
        }
        
        public override void Process(Entity entity)
        {
            var triggerDetector = entity.GetComponent<TriggerDetectorComponent>();
            if (triggerDetector.CollidingObjects.Count > 0) 
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
