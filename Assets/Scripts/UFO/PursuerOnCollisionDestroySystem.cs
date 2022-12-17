using System;
using System.Collections.Generic;
using System.Linq;
using Asteroids.Collisions;
using ECS;
using ECS.Messages;
using UnityEngine;

namespace Asteroids.UFO
{
    public class PursuerOnCollisionDestroySystem : ECS.System
    {
        public override IEnumerable<Type> ComponentsMask => _componentsMask;
        
        private IEnumerable<Type> _componentsMask = new List<Type>()
        {
            typeof(PlayerPursuerComponent),
            typeof(CollisionDetectorComponent)
        };
        
        private int _laserLayerMask;
        private int _playerLayerMask;

        public PursuerOnCollisionDestroySystem()
        {
            _laserLayerMask = LayerMask.NameToLayer("Laser");
            _playerLayerMask = LayerMask.NameToLayer("Player");
        }
        
        public override void Process(Entity entity)
        {
            var collisionDetector = entity.GetComponent<CollisionDetectorComponent>();

            if (collisionDetector.CollidingObjects.Count == 0) return;

            if (collisionDetector.CollidingObjects.Any(go => go.layer != _playerLayerMask))
            {
                MessageDispatcher.SendMessage(MessageType.PlayerPursuerKilled, entity);
                entity.InitDestroy();   
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
