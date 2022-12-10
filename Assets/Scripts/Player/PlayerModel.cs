using System;
using Asteroids.DeathTracker;
using Asteroids.Movable;
using Asteroids.RegularWeapon;
using Core;

namespace Asteroids.Player
{
    public class PlayerModel : Model
    {
        public float RotationForce => InputObserver.RotationInputValue * _rotationSpeed;
        public MovableFacade Movable { get; private set; }
        public IPlayerInputObserver InputObserver { get; private set; }
        public DeathTrackerFacade DeathTracker { get; private set; }

        public RegularWeaponFacade RegularWeapon { get; private set;}

        private float _rotationSpeed;
        
        public PlayerModel(MovableFacade movable, IPlayerInputObserver inputObserver, float rotationSpeed, 
            DeathTrackerFacade deathTracker, RegularWeaponFacade regularWeapon)
        {
            Movable = movable;
            InputObserver = inputObserver;
            _rotationSpeed = rotationSpeed;
            DeathTracker = deathTracker;
            RegularWeapon = regularWeapon;
        }
    }
}
