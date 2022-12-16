using System;
using System.Collections.Generic;
using Asteroids.Movable;
using ECS;
using ECS.Messages;
using UnityEngine;

namespace Asteroids.Player
{
    public class PlayerSpatialDataNotifierSystem : ECS.System
    {
        public override IEnumerable<Type> ComponentsMask => _componentsMask;

        private IEnumerable<Type> _componentsMask;

        private Tuple<Vector2, float, Vector2> spatialData;

        public PlayerSpatialDataNotifierSystem()
        {
            _componentsMask = new List<Type>()
            {
                typeof(PlayerComponent),
                typeof(MovableComponent)
            };
        }
        
        public override void Process(Entity entity)
        {
            var movableComponent = entity.GetComponent<MovableComponent>();

            spatialData = new Tuple<Vector2, float, Vector2>(movableComponent.Position,
                movableComponent.Rotation.eulerAngles.z - 180f, movableComponent.Velocity);
            MessageDispatcher.SendMessage(MessageType.PlayerSpatialDataChanged, spatialData);
        }

        public override void PostProcess()
        {
        }

        public override void Destroy()
        {
        }
    }
}
