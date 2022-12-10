using System;
using UnityEngine.InputSystem;

namespace Asteroids.Player
{
    public class PlayerInputObserver: IPlayerInputObserver
    {
        public EventHandler<bool> ThrustInputDetected { get; set; }
        public EventHandler FireInputDetected { get; set; }
        public EventHandler LaserFireInputDetected { get; set; }
        public float RotationInputValue => _inputActions.Player.Rotation.ReadValue<float>();

        private InputActions _inputActions;

        public PlayerInputObserver()
        {
            _inputActions = new InputActions();
            _inputActions.Player.Enable();

            _inputActions.Player.Thrust.started += HandleThrustInput;
            _inputActions.Player.Thrust.canceled += HandleThrustInput;
            _inputActions.Player.Fire.started += HandleFireInput;
        }

        ~PlayerInputObserver()
        {
            _inputActions.Player.Thrust.started -= HandleThrustInput;
            _inputActions.Player.Thrust.canceled -= HandleThrustInput;
            _inputActions.Player.Fire.started -= HandleFireInput;
        }

        private void HandleThrustInput(InputAction.CallbackContext context)
        {
            if (context.started)
                ThrustInputDetected?.Invoke(this, true);
            
            if (context.canceled)
                ThrustInputDetected?.Invoke(this, false);
        }
        
        private void HandleFireInput(InputAction.CallbackContext context)
        {
            FireInputDetected?.Invoke(this, null);
        }
        
        private void HandleLaserFireInput(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }
        
    }
}
