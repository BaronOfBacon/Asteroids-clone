using Core;
using UnityEngine;

namespace Asteroids.RegularWeapon
{
    public class RegularWeaponPresenter : Presenter<RegularWeaponModel, RegularWeaponView>
    {
        
       
        public RegularWeaponPresenter(RegularWeaponModel model, RegularWeaponView view) : base(model, view)
        {
            
        }
        
        public void TryShoot(Vector2 direction)
        {
            var projectilePosition = view.transform.position +
                                     view.transform.rotation * model.ProjectileSpawnOffset;

            var projectileVelocity = direction * model.ProjectileSpeed;
            
            var projectileFacade = model.ProjectileFactory.Create((Vector2)projectilePosition, projectileVelocity);
        }
    }
}
