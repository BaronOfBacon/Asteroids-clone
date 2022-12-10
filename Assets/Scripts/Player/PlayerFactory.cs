using System;
using Asteroids.DeathTracker;
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

        public PlayerFactory(MovableFactory movableFactory, DeathTrackerFactory deathTrackerFactory, 
            RegularWeaponFactory regularWeaponFactory)
        {
            _movableFactory = movableFactory;
            _deathTrackerFactory = deathTrackerFactory;
            _regularWeaponFactory = regularWeaponFactory;
        }
        
        /// <summary>
        /// Creates Player entity.
        /// </summary>
        /// <param name="args">[0] GameObject (prefab), [1] MovableData (Init data), [2] IPlayerInputObserver,
        /// [3] float (Player rotation speed), [4] GameObject (Projectile prefab), [5] float (Projectile speed),
        /// [6] ProjectileFactory</param>
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
            
            var gameObject = GameObject.Instantiate(prefab);

            var movableFacade = _movableFactory.Create(movableData, gameObject, false);
            var deathTrackerFacade = _deathTrackerFactory.Create(gameObject, false);
            var regularWeaponView = gameObject.GetComponentInChildren<RegularWeaponView>();
            
            var regularWeaponFacade = _regularWeaponFactory.Create(projectilePrefab, regularWeaponView, 
                projectileSpeed, projectileFactory); 
            
            var view = gameObject.GetComponentInChildren<PlayerView>();
            var model = new PlayerModel(movableFacade, inputObserver, playerRotationSpeed, deathTrackerFacade, regularWeaponFacade);
            var presenter = new PlayerPresenter(model, view);
            
            return new PlayerFacade(presenter);
        }
    }
}
