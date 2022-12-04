using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;

namespace Asteroids.MovableSystem
{
    public class MovableSystemView : View
    {
        public EventHandler UpdateCalled;
        public GameObject[] _movables;

        private void Update()
        {
            UpdateCalled?.Invoke(null, null);
        }

        public void UpdateMovablesList(IList<GameObject> movables)
        {
            _movables = movables.ToArray();
        }
    }
}