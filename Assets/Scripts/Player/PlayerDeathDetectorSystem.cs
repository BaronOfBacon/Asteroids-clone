
using System;
using System.Collections.Generic;
using System.Linq;
using Asteroids.Collisions;
using ECS;
using ECS.Messages;
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
                typeof(CollisionDetectorComponent)
            };
        }
        
        public override void Process(Entity entity)
        {
            var collisionDetector = entity.GetComponent<CollisionDetectorComponent>();
            if (collisionDetector.CollidingObjects.Any())
            {
                MessageDispatcher.SendMessage(MessageType.PlayerDied, null);
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
