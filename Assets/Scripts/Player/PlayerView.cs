using System;
using Core;

namespace Asteroids.Player
{
    public class PlayerView : View
    {
        public EventHandler OnUpdate;
        
        private void Update()
        {
            OnUpdate?.Invoke(this, null);
        }
    }
}