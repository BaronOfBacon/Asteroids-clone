using System.Collections.Generic;
using UnityEngine;
using Component = ECS.Component;

namespace Asteroids.Collisions
{
    public class TriggerDetectorComponent : Component
    {
        public IReadOnlyList<GameObject> CollidingObjects => _collidingObjects;
        
        private List<GameObject> _collidingObjects = new List<GameObject>();

        private void OnTriggerEnter2D(object sender, Collider2D collider)
        {
            if (_collidingObjects.Contains(collider.gameObject))
                return;
            
            _collidingObjects.Add(collider.gameObject);
        }

        private void OnTriggerExit2D(object sender, Collider2D collider)
        {
            if (!_collidingObjects.Contains(collider.gameObject))
                return;
            _collidingObjects.Remove(collider.gameObject);
        }

        public void SubscribeDetector(TriggerDetector2D detector2D)
        {
            detector2D.TriggerEnter2D += OnTriggerEnter2D;
            detector2D.TriggerExit2D += OnTriggerExit2D;
        }

        public void UnsubscribeDetector(TriggerDetector2D detector2D)
        {
            detector2D.TriggerEnter2D -= OnTriggerEnter2D;
            detector2D.TriggerExit2D -= OnTriggerExit2D;
        }
        
    }
}
