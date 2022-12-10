using Asteroids.Movable;
using Core;

namespace Asteroids.Weapon.Projectile
{
    public class ProjectileModel : Model
    {
        public MovableFacade MovableFacade { get; private set; }
        
        public ProjectileModel(MovableFacade movableFacade)
        {
            MovableFacade = movableFacade;
        }
    }
}
