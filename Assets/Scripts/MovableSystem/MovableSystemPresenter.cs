using System;
using System.Linq;
using Asteroids.Movable;
using Core;
using UnityEngine;

namespace Asteroids.MovableSystem
{
    public class MovableSystemPresenter : Presenter<MovableSystemModel, MovableSystemView>
    {
        private FieldCalculationHelper _fieldCalculationHelper;

        public MovableSystemPresenter(MovableSystemModel model, MovableSystemView view) : base(model, view)
        {
            model.MovablesChanged += HandleMovablesChanged;
            view.UpdateCalled += Update;
        }

        public void Init()
        {
            _fieldCalculationHelper = new FieldCalculationHelper(model.fieldBoundariesDistance);
        }
        
        private void Update(object sender, EventArgs args)
        {
            foreach (var movable in model.Movables)
            {
                movable.Transform.rotation = movable.Rotation;
                
                if (movable.AccelerateForward)
                {
                    movable.Acceleration =  model.forwardAccelerationMultiplier * movable.Transform.up;
                }
                else if (movable.Acceleration != Vector2.zero)
                    movable.Acceleration = Vector2.zero;
                
                if (movable.Acceleration != Vector2.zero)
                {
                    movable.Velocity += movable.Acceleration * Time.deltaTime;
                    
                }

                var timeDependentFriction = movable.Friction * Time.deltaTime;
                var speedWithFriction = movable.Velocity.magnitude - timeDependentFriction;
                var maxSpeed = Mathf.Clamp(speedWithFriction, 0, model.maxSpeed);
                movable.Velocity = Vector3.ClampMagnitude(movable.Velocity, maxSpeed);

                var newPosition = movable.Position + movable.Velocity * Time.deltaTime;

                if (!_fieldCalculationHelper.IsInsideOfBoundaries(newPosition))
                {
                    if (!_fieldCalculationHelper.NewPositionInPortal(out newPosition, movable.Position,
                        movable.Velocity))
                    {
                        Debug.LogError("Can't calculate new position!");
                    }
                }
                
                movable.Position = newPosition;
            }
        }

        private void HandleMovablesChanged(object sender, EventArgs args)
        {
            var movables = from movable in model.Movables
                select movable.Transform.gameObject;
            view.UpdateMovablesList(movables.ToArray());
        }
        
        public void AddMovable(MovableFacade movable)
        {
            model.AddMovable(movable);
        }

        public void DeleteMovable(MovableFacade movable)
        {
            model.DeleteMovable(movable);
        }
        
        ~MovableSystemPresenter()
        {
            model.MovablesChanged -= HandleMovablesChanged;
        }
    }
}
