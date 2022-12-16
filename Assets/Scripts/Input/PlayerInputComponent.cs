using Component = ECS.Component;

namespace Asteroids.Input
{
    public class PlayerInputComponent : Component
    {
        public bool Thrust;
        public float Rotation;
        public bool Fire;
        public bool LaserFire;
    }
}
