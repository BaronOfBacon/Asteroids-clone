using Core;

namespace Asteroids.LaserWeapon
{
    public class LaserWeaponFacade : Facade<LaserWeaponModel, LaserWeaponView>, IWeapon
    {
        private LaserWeaponPresenter _laserWeaponPresenter;
        public LaserWeaponFacade(Presenter<LaserWeaponModel, LaserWeaponView> presenter) : base(presenter)
        {
            _laserWeaponPresenter = (LaserWeaponPresenter)presenter;
        }

        public void TryShoot()
        {
            _laserWeaponPresenter.TryShoot();
        }
    }
}