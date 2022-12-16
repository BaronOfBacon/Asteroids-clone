using UnityEngine;
using Component = ECS.Component;

namespace Asteroids.UI.Game
{
    public class GameUIComponent : Component
    {
        public GameUIPanelView Panel { get; private set; }

        public Vector2 Position;
        public float Angle;
        public Vector2 Velocity;
        public int Charges;
        public float Cooldown;
        
        public GameUIComponent(GameUIPanelView panel)
        {
            Panel = panel;
        }

    }
}
