using System;
using UnityEngine;

namespace Asteroids.Collisions
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class TriggerDetector2D : MonoBehaviour
    {
        public EventHandler<Collider2D> TriggerEnter2D;
        public EventHandler<Collider2D> TriggerExit2D;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            TriggerEnter2D?.Invoke(gameObject, other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            TriggerExit2D?.Invoke(gameObject, other);
        }
    }
}
