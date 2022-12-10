using Asteroids.Weapon.Projectile;
using Core;
using UnityEngine;

namespace Asteroids.RegularWeapon
{
    public class RegularWeaponFactory : AbstractObjectFactory<RegularWeaponFacade>
    {
        /// <summary>
        /// Creates regular weapon entity.
        /// </summary>
        /// <param name="args">[0] GameObject (projectilePrefab), [1]RegularWeaponView (must contain RegularWeaponView),
        /// [2]Vector2 (Projectile speed), [3] ProjectileFactory</param>
        /// <returns>Returns movable facade.</returns>
        public override RegularWeaponFacade Create(params object[] args)
        {
            var projectilePrefab = (GameObject)args[0];
            var view = (RegularWeaponView)args[1];
            var projectileSpawnOffset = view.ProjectileSpawnOffset;
            var projectileSpeed = (float)args[2];
            var projectileFactory = (ProjectileFactory)args[3];
            var model = new RegularWeaponModel(projectileSpawnOffset, projectilePrefab, projectileSpeed, projectileFactory);
            var presenter = new RegularWeaponPresenter(model, view);
            
            return new RegularWeaponFacade(presenter);
        }
    }
}
