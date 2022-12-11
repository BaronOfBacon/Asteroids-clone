using Core;
using UnityEngine;

namespace Asteroids.RegularWeapon
{
    public class RegularWeaponPresenter : Presenter<RegularWeaponModel, RegularWeaponView>
    {
        
       
        public RegularWeaponPresenter(RegularWeaponModel model, RegularWeaponView view) : base(model, view)
        {
            
        }
        
        public void TryShoot()
        {
            var projectilePosition = view.transform.position +
                                     view.transform.rotation * model.ProjectileSpawnOffset;

            var projectileVelocity = view.transform.up * model.ProjectileSpeed;
            
            var projectileFacade = model.ProjectileFactory.Create(projectilePosition, projectileVelocity);
        }
    }
}
