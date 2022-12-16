using System;
using UnityEngine;

namespace Asteroids.Collisions
{
    public class CollisionDetector2D : MonoBehaviour
    {
        public EventHandler<Collider2D> CollisionEnter2D;
        public EventHandler<Collider2D> CollisionExit2D;

        private void OnCollisionEnter2D(Collision2D other)
        {
            CollisionEnter2D?.Invoke(this, other.collider);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            CollisionExit2D?.Invoke(this, other.collider);
        }
    }
}
