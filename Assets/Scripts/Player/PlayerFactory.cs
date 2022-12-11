using Asteroids.DeathTracker;
using Asteroids.Helpers;
using Asteroids.LaserWeapon;
using Asteroids.Movable;
using Asteroids.RegularWeapon;
using Asteroids.Weapon.Projectile;
using Core;
using UnityEngine;

namespace Asteroids.Player
{
    public class PlayerFactory : AbstractObjectFactory<PlayerFacade>
    {
        private MovableFactory _movableFactory;
        private DeathTrackerFactory _deathTrackerFactory;
        private RegularWeaponFactory _regularWeaponFactory;
        private LaserWeaponFactory _laserWeaponFactory;
        private FieldCalculationHelper _fieldCalculationHelper;

        public PlayerFactory(MovableFactory movableFactory, DeathTrackerFactory deathTrackerFactory, 
            RegularWeaponFactory regularWeaponFactory, LaserWeaponFactory laserWeaponFactory, FieldCalculationHelper fieldCalculationHelper)
        {
            _movableFactory = movableFactory;
            _deathTrackerFactory = deathTrackerFactory;
            _regularWeaponFactory = regularWeaponFactory;
            _laserWeaponFactory = laserWeaponFactory;
            _fieldCalculationHelper = fieldCalculationHelper;
        }
        
        /// <summary>
        /// Creates Player entity.
        /// </summary>
        /// <param name="args">[0] GameObject (prefab), [1] MovableData (Init data), [2] IPlayerInputObserver,
        /// [3] float (Player rotation speed), [4] GameObject (Projectile prefab), [5] float (Projectile speed),
        /// [6] ProjectileFactory, [7] float (Laser active time duration), [8] int (Laser charges capacity),
        /// [9] float (Laser charge cooldown time)</param>
        /// <returns>PlayerFacade</returns>
        public override PlayerFacade Create(params object[] args)
        {
            var prefab = (GameObject)args[0];
            var movableData = (MovableData)args[1];
            var inputObserver = (IPlayerInputObserver)args[2];
            var playerRotationSpeed = (float)args[3];
            var projectilePrefab = (GameObject)args[4];
            var projectileSpeed = (float)args[5];
            var projectileFactory = (ProjectileFactory)args[6];
            var laserActiveTimeDuration = (float)args[7];
            var laserChargesCapacity = (int)args[8];
            var laserChargeCoolDownTime = (float)args[9];

            var gameObject = GameObject.Instantiate(prefab);

            var movableFacade = _movableFactory.Create(movableData, gameObject, false);
            var deathTrackerFacade = _deathTrackerFactory.Create(gameObject, false);
            
            var regularWeaponView = gameObject.GetComponentInChildren<RegularWeaponView>();
            var regularWeaponFacade = _regularWeaponFactory.Create(projectilePrefab, regularWeaponView, 
                projectileSpeed, projectileFactory);

            var laserWeaponView = gameObject.GetComponentInChildren<LaserWeaponView>();
            var laserWeaponFacade = _laserWeaponFactory.Create(laserWeaponView, _fieldCalculationHelper, 
                laserActiveTimeDuration, laserChargesCapacity, laserChargeCoolDownTime);
            
            var view = gameObject.GetComponentInChildren<PlayerView>();
            var model = new PlayerModel(movableFacade, inputObserver, playerRotationSpeed, deathTrackerFacade, 
                regularWeaponFacade, laserWeaponFacade);
            var presenter = new PlayerPresenter(model, view);
            
            return new PlayerFacade(presenter);
        }
    }
}
