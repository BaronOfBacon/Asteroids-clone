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
            set
            {
                _data.position = value;
                //notify movable presenter about mass change
            }
        }
        public Quaternion Rotation
        {
            get => _data.rotation;
            set
            {
                _data.rotation = value;
                //notify movable presenter about mass change
            }
        }
        public Vector2 Velocity
        {
            get => _data.velocity;
            set
            {
                _data.velocity = value;
                //notify movable presenter about Velocity change
            }
        }
        public Vector2 Acceleration
        {
            get => _data.acceleration;
            set
            {
                _data.acceleration = value;
                //notify movable presenter about Velocity change
            }
        }

        private MovableData _data;

        public MovableModel(MovableData data)
        {
            _data.mass = data.mass;
            _data.position = data.position;
            _data.rotation = data.rotation;
            _data.velocity = data.velocity;
            _data.acceleration = data.acceleration;
        }
    }
}
