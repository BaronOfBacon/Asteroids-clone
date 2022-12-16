
using System;
using System.Collections.Generic;
using System.Linq;
using Asteroids.Collisions;
using ECS;
using UnityEngine;

namespace Asteroids.Player
{
    public class PlayerDeathDetectorSystem : ECS.System
    {
        public override IEnumerable<Type> ComponentsMask => _componentsMask;

        private readonly IEnumerable<Type> _componentsMask;
        
        public PlayerDeathDetectorSystem()
        {
            _componentsMask = new List<Type>()
            {
                typeof(PlayerComponent),
                typeof(TriggerDetectorComponent)
            };
        }
        
        public override void Process(Entity entity)
        {
            var triggerDetector = entity.GetComponent<TriggerDetectorComponent>();
            if (triggerDetector.CollidingObjects.Any())
            {
                Debug.Log("Player died");
            }
        }

        public override void PostProcess()
        {
            
        }

        public override void Destroy()
        {
            
        }
    }
}
