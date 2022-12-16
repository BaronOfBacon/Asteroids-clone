using System;
using System.Collections.Generic;
using ECS;
using ECS.Messages;
using UnityEngine;

namespace Asteroids.UI.Game
{
    public class GameUISystem : ECS.System
    {
        public override IEnumerable<Type> ComponentsMask => _componentsMask;
        
        private IEnumerable<Type> _componentsMask;

        private Vector2 position;
        private float angle;
        private Vector2 velocity;
        private int charges;
        private float cooldown;

        public GameUISystem()
        {
            _componentsMask = new List<Type>()
            {
                typeof(GameUIComponent)
            };
        }

        public override void Initialize(World world, Dispatcher messageDispatcher)
        {
            base.Initialize(world, messageDispatcher);
            MessageDispatcher.Subscribe(MessageType.PlayerSpatialDataChanged, HandleSpatialDataChange);
            MessageDispatcher.Subscribe(MessageType.LaserDataChanged, HandleLaserDataChange);
        }

        public override void Process(Entity entity)
        {
            var component = entity.GetComponent<GameUIComponent>();
            
            if (component.Position != position)
            {
                component.Position = position;
                component.Panel.UpdateCoordinates(position);
            }
            if (component.Angle != angle)
            {
                component.Angle = angle;
                component.Panel.UpdateAngle(angle);
            }
            if (component.Velocity != velocity)
            {
                component.Velocity = velocity;
                component.Panel.UpdateSpeed(velocity);
            }
            if (component.Charges != charges)
            {
                component.Charges = charges;
                component.Panel.UpdateLaserCharges(charges);
            }
            if (component.Cooldown != cooldown)
            {
                component.Cooldown = cooldown;
                component.Panel.UpdateLaserChargesCooldown(cooldown);
            }
        }

        public override void PostProcess()
        {
        }

        private void HandleSpatialDataChange(object spatialData)
        {
            var data = (Tuple<Vector2, float, Vector2>)spatialData;
            position = data.Item1;
            angle = data.Item2;
            velocity = data.Item3;
        }
        
        private void HandleLaserDataChange(object laserData)
        {
           var data = (Tuple<int, float>)laserData;
           charges = data.Item1;
           cooldown = data.Item2;
        }
        
        public override void Destroy()
        {
            MessageDispatcher.Unsubscribe(MessageType.PlayerSpatialDataChanged, HandleSpatialDataChange);
            MessageDispatcher.Subscribe(MessageType.PlayerSpatialDataChanged, HandleSpatialDataChange);
        }
    }
}
