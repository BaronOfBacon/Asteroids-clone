using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids.Input;
using Asteroids.Movable;
using ECS;
using UnityEngine;

namespace Asteroids.Player
{
    public class PlayerMoveSystem : ECS.System
    {
        public override IEnumerable<Type> ComponentsMask => new List<Type>()
        {
            typeof(PlayerInputComponent),
            typeof(MovableComponent),
        };

        private readonly float _rotationSpeed;

        public PlayerMoveSystem(float rotationSpeed)
        {
            _rotationSpeed = rotationSpeed;
        }
        public override void Process(Entity entity)
        {
           var movableComponent = entity.GetComponent<MovableComponent>();
           var playerInputComponent = entity.GetComponent<PlayerInputComponent>();

           movableComponent.AccelerateForward = playerInputComponent.Thrust;

           var deltaRotation = playerInputComponent.Rotation * _rotationSpeed * Vector3.forward;
           movableComponent.Rotation *= Quaternion.Euler(deltaRotation);
        }

        public override void PostProcess()
        {
            
        }

        public override void Destroy()
        {
           
        }
    }
}
