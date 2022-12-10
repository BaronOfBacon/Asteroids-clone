using Core;
using UnityEngine;

namespace Asteroids.Movable
{
    public class MovableModel : Model
    {
        public MovableData Data => _data;
        public float Mass => _data.mass;
        public Vector2 Position
        {
            get => _data.position;
            set => _data.position = value;
        }
        public Quaternion Rotation
        {
            get => _data.rotation;
            set => _data.rotation = value;
        }
        public Vector2 Velocity
        {
            get => _data.velocity;
            set => _data.velocity = value;
        }
        public Vector2 Acceleration
        {
            get => _data.acceleration;
            set => _data.acceleration = value;
        }
        public bool AccelerateForward
        {
            get => _data.accelerateForward;
            set => _data.accelerateForward = value;
        }
        public float Friction
        {
            get => _data.friction;
            set => _data.friction = value;
        }
        public bool DestroyOutsideTheField
        {
            get => _data.destroyOutsideTheField;
            set => _data.destroyOutsideTheField = value;
        }

        private MovableData _data;

        public MovableModel(MovableData data)
        {
            _data = data;
        }
    }
}
