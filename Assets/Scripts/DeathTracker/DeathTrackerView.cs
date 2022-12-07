using System;
using Core;
using UnityEngine;

namespace Asteroids.DeathTracker
{
    public class DeathTrackerView : View
    {
        public EventHandler Death;

        private void OnTriggerEnter2D(Collider2D other)
        {
            Death?.Invoke(this, null);
        }
    }
}