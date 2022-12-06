using Core;
using UnityEngine;

namespace Asteroids.Movable
{
    public class MovableView : View
    {
        public Transform Transform => transform;

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
