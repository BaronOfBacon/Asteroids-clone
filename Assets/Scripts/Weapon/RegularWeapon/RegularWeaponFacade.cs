using Core;
using UnityEngine;

namespace Asteroids.RegularWeapon
{
    public class RegularWeaponFacade : Facade<RegularWeaponModel, RegularWeaponView>, IWeapon
    {
        private RegularWeaponPresenter _regularWeaponPresenter;
       
        public RegularWeaponFacade(Presenter<RegularWeaponModel, RegularWeaponView> presenter) : base(presenter)
        {
            _regularWeaponPresenter = (RegularWeaponPresenter)presenter;
        }

        public void TryShoot(Vector2 direction)
        {
            _regularWeaponPresenter.TryShoot(direction);
        }
    }
}