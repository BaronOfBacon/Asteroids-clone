using System;
using System.Linq;
using Asteroids.Movable;
using Core;
using UnityEngine;

namespace Asteroids.MovableSystem
{
    public class MovableSystemPresenter : Presenter<MovableSystemModel, MovableSystemView>
    {
        public MovableSystemPresenter(MovableSystemModel model, MovableSystemView view) : base(model, view)
        {
            model.MovablesChanged += HandleMovablesChanged;
            view.UpdateCalled += Update;
        }

        private void Update(object sender, EventArgs args)
        {
            foreach (var movable in model.Movables)
            {
                movable.Position += movable.Velocity * Time.deltaTime;
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
