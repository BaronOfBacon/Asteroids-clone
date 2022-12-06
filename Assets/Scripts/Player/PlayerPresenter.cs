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
        }

        ~PlayerPresenter()
        {
            model.InputObserver.ThrustInputDetected -= HandleThrust;
            view.OnUpdate -= Update;
        }

        private void BindToInputObserver()
        {
            model.InputObserver.ThrustInputDetected += HandleThrust;
        }

        private void HandleThrust(object sender, bool state)
        {
            model.Movable.AccelerateForward = state;
        }

        private void Update(object sender, EventArgs args)
        {
            model.Movable.Rotation *= Quaternion.Euler(Vector3.forward * model.RotationForce);
        }
    }
}
