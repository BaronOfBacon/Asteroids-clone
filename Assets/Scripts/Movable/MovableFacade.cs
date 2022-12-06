using System;
using Core;
using UnityEngine;

namespace Asteroids.Movable
{
    public class MovableFacade : Facade<MovableModel, MovableView>
    {
        public Action<MovableFacade> OnDestroy;
        public Transform Transform => _presenter.Transform;
        public float Mass => _presenter.Mass;
        public Vector2 Position
        {
            get => _presenter.Position;
            set => _presenter.Position = value;
        }
        public Quaternion Rotation
        {
            get => _presenter.Rotation;
            set => _presenter.Rotation = value;
        }
        public Vector2 Velocity
        {
            get => _presenter.Velocity;
            set => _presenter.Velocity = value;
        }
        public Vector2 Acceleration
        {
            get => _presenter.Acceleration;
            set => _presenter.Acceleration = value;
        }

        public bool AccelerateForward
        {
            get => _presenter.AccelerateForward;
            set => _presenter.AccelerateForward = value;
        }
        
        private MovablePresenter _presenter;
        
        public MovableFacade(Presenter<MovableModel, MovableView> presenter) : base(presenter)
        {
            _presenter = presenter as MovablePresenter;
        }

        public override void Destroy()
        {
            OnDestroy.Invoke(this);
            
            _presenter.Destroy();
        }
    }
}
