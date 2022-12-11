using System;
using Core;
using UnityEngine;

namespace Asteroids.Player
{
    public class PlayerPresenter : Presenter<PlayerModel, PlayerView>
    {

        public PlayerPresenter(PlayerModel model, PlayerView view) : base(model, view)
        {
            BindToInputObserver();
            view.OnUpdate += Update;
            model.DeathTracker.Death += HandleDeath;
            
        }

        ~PlayerPresenter()
        {
            view.OnUpdate -= Update;
            model.InputObserver.ThrustInputDetected -= HandleThrust;
            model.InputObserver.RegularFireInputDetected -= HandleFire;
            model.InputObserver.LaserFireInputDetected -= HandleLaserFire;
        }

        private void BindToInputObserver()
        {
            model.InputObserver.ThrustInputDetected += HandleThrust;
            model.InputObserver.RegularFireInputDetected += HandleFire;
            model.InputObserver.LaserFireInputDetected += HandleLaserFire;
        }

        private void Update(object sender, EventArgs args)
        {
            model.Movable.Rotation *= Quaternion.Euler(Vector3.forward * model.RotationForce);
        }

        private void HandleThrust(object sender, bool state)
        {
            model.Movable.AccelerateForward = state;
        }
        
        private void HandleDeath(object sender, EventArgs args)
        {
            Debug.Log("Player died!");
        }

        private void HandleFire(object sender, EventArgs args)
        {
            model.RegularWeapon.TryShoot();
        }

        private void HandleLaserFire(object sender, EventArgs args)
        {
            model.LaserWeapon.TryShoot();
        }
    }
}
