using Core;
using UnityEngine;

namespace Asteroids.Movable
{
    public class MovablePresenter : Presenter<MovableModel, MovableView>
    {
        public Transform Transform => view.transform;
        public float Mass => model.Mass;
        public Vector2 Position
        {
            get => model.Position;
            set
            {
                model.Position = value;
                view.transform.position = model.Position;
            }
        }

        public Quaternion Rotation
        {
            get => model.Rotation;
            set => model.Rotation = value;
        }
        public Vector2 Velocity
        {
            get => model.Velocity;
            set => model.Velocity = value;
        }
        public Vector2 Acceleration
        {
            get => model.Acceleration;
            set => model.Acceleration = value;
        }
        
        public MovablePresenter(MovableModel model, MovableView view) : base(model, view)
        {
            
        }

        public override void Destroy()
        {
            view.Destroy();
        }
    }
}
