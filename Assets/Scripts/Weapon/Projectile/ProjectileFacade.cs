using Core;

namespace Asteroids.Weapon.Projectile
{
    public class ProjectileFacade : Facade<ProjectileModel, ProjectileView>
    {
        public ProjectileFacade(Presenter<ProjectileModel, ProjectileView> presenter) : base(presenter)
        {
        }
    }
}