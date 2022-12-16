using Component = ECS.Component;

namespace Asteroids.Asteroid
{
    public class AsteroidComponent : Component
    {
        public bool IsFraction { get; }

        public AsteroidComponent(bool isFraction)
        {
            IsFraction = isFraction;
        }
    }
}
