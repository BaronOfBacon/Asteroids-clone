using System.Collections.Generic;
using UnityEngine;
using Component = ECS.Component;

namespace Asteroids.Collisions
{
    public class CollisionDetectorComponent : Component
    {
        public IReadOnlyList<GameObject> CollidingObjects => _collidingObjects;
        
        private List<GameObject> _collidingObjects = new List<GameObject>();

        private void OnCollisionEnter2D(object sender, Collider2D collider)
        {
            if (_collidingObjects.Contains(collider.gameObject))
                return;
            
            _collidingObjects.Add(collider.gameObject);
        }

        private void OnCollisionExit2D(object sender, Collider2D collider)
        {
            if (!_collidingObjects.Contains(collider.gameObject))
                return;
            _collidingObjects.Remove(collider.gameObject);
        }

        public void SubscribeDetector(CollisionDetector2D detector2D)
        {
            detector2D.CollisionEnter2D += OnCollisionEnter2D;
            detector2D.CollisionExit2D += OnCollisionExit2D;
        }

        public void UnsubscribeDetector(CollisionDetector2D detector2D)
        {
            detector2D.CollisionEnter2D -= OnCollisionEnter2D;
            detector2D.CollisionExit2D -= OnCollisionExit2D;
        }
        
    }
}
