using UnityEngine;

namespace Asteroids.Movable
{
    public struct MovableData 
    {
        public float mass;
        public Vector2 position;
        public Quaternion rotation;
        public Vector2 velocity;
        public Vector2 acceleration;
        public bool accelerateForward;
    }
}
