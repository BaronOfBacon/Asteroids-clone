using System;
using System.Collections.Generic;
using Asteroids.Helpers;
using ECS;
using UnityEngine;

namespace Asteroids.Movable
{
    public class MovableSystem : ECS.System
    {
        public override IEnumerable<Type> ComponentsMask =>
            new List<Type>()
            {
                typeof(MovableComponent)
            };

        private float _forwardAccelerationMultiplier;
        private float _maxSpeed;
        private FieldCalculationHelper _fieldCalculationHelper;

        public MovableSystem(float forwardAccelerationMultiplier, float maxSpeed
            , FieldCalculationHelper fieldCalculationHelper)
        {
            _forwardAccelerationMultiplier = forwardAccelerationMultiplier;
            _maxSpeed = maxSpeed;
            _fieldCalculationHelper = fieldCalculationHelper;
        }
        
        public override void Process(Entity entity)
        {
            MovableComponent movableComponent = entity.GetComponent<MovableComponent>();

            entity.GameObject.transform.rotation = movableComponent.Rotation;

            if (movableComponent.AccelerateForward)
            {
                movableComponent.Acceleration = _forwardAccelerationMultiplier * entity.GameObject.transform.up;
            }
            else
            {
                if (movableComponent.Acceleration != Vector2.zero)
                {
                    movableComponent.Acceleration = Vector2.zero;
                }
            }
            
            if (movableComponent.Acceleration != Vector2.zero)
            {
                movableComponent.Velocity += movableComponent.Acceleration * Time.deltaTime;
            }
            
            
            var timeDependentFriction = movableComponent.Friction * Time.deltaTime;
            var speedWithFriction = movableComponent.Velocity.magnitude - timeDependentFriction;
            var maxSpeed = Mathf.Clamp(speedWithFriction, 0, _maxSpeed);
            movableComponent.Velocity = Vector3.ClampMagnitude(movableComponent.Velocity, maxSpeed);
            
            var newPosition = movableComponent.Position + movableComponent.Velocity * Time.deltaTime;
            
            
            if (!_fieldCalculationHelper.IsInsideOfBoundaries(newPosition))
            {
                if (movableComponent.DestroyOutsideTheField)
                {
                    entity.InitDestroy();
                }
                    
                if (!_fieldCalculationHelper.NewPositionInPortal(out newPosition, movableComponent.Position,
                    movableComponent.Velocity))
                {
                    Debug.LogError("Can't calculate new position!");
                }
            }
                
            movableComponent.Position = newPosition;

            entity.GameObject.transform.position = movableComponent.Position;
            entity.GameObject.transform.rotation = movableComponent.Rotation;
        }

        public override void PostProcess()
        {
        }

        public override void Destroy()
        {
            
        }
    }
}
