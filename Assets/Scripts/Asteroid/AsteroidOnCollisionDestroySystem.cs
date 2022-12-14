using System;
using System.Collections.Generic;
using System.Linq;
using Asteroids.Collisions;
using ECS;
using ECS.Messages;
using UnityEngine;

namespace Asteroids.Asteroid
{
    public class AsteroidOnCollisionDestroySystem : ECS.System
    {
        public override IEnumerable<Type> ComponentsMask => _componentsMask;

        private readonly IEnumerable<Type> _componentsMask = new List<Type>()
        {
            typeof(AsteroidComponent),
            typeof(CollisionDetectorComponent)
        };

        private int _laserLayerMask;
        private int _playerLayerMask;
        
        public AsteroidOnCollisionDestroySystem()
        {
            _laserLayerMask = LayerMask.NameToLayer("Laser");
            _playerLayerMask = LayerMask.NameToLayer("Player");
        }
        
        public override void Process(Entity entity)
        {
            var collisionDetector = entity.GetComponent<CollisionDetectorComponent>();

            if (collisionDetector.CollidingObjects.Count == 0) return;

            var asteroidComponent = entity.GetComponent<AsteroidComponent>();
            
            if (!asteroidComponent.IsFraction && 
                collisionDetector.CollidingObjects.All(go => go.layer != _laserLayerMask 
                                                           && go.layer != _playerLayerMask))
            {
                Tuple<Vector3, Vector3> positionAndDirection = new Tuple<Vector3, Vector3>(
                    entity.GameObject.transform.position,
                    entity.GameObject.transform.up);
                MessageDispatcher.SendMessage(MessageType.SpawnAsteroidFragments, positionAndDirection);
            }

            if (collisionDetector.CollidingObjects.Any(go => go.layer != _playerLayerMask))
            {
                MessageDispatcher.SendMessage(MessageType.AsteroidKilled, asteroidComponent);
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
