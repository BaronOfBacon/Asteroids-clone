using System;
using System.Collections.Generic;
using Asteroids.Input;
using ECS;
using UnityEngine.InputSystem;

namespace Asteroids.Player
{
    public class PlayerInputSystem : ECS.System
    {
        public override IEnumerable<Type> ComponentsMask
        {
            get => new List<Type>()
            {
                typeof(PlayerInputComponent)
            };
        }

        private InputActions _inputActions;
        private float _rotationSpeed;

        private bool _fire;
        private bool _laserFire;
        
        public PlayerInputSystem(InputActions inputActions)
        {
            _inputActions = inputActions;
            _inputActions.Player.Enable();
            _inputActions.Player.Fire.started += Fire;
            _inputActions.Player.LaserFire.started += LaserFire;
        }
        public override void Process(Entity entity)
        {
            var inputComponent = entity.GetComponent<PlayerInputComponent>();
            
            inputComponent.Thrust = _inputActions.Player.Thrust.ReadValue<float>() > 0;
            inputComponent.Rotation = _inputActions.Player.Rotation.ReadValue<float>();
            inputComponent.Fire = _fire;
            inputComponent.LaserFire = _laserFire;
        }

        public override void PostProcess()
        {
            _fire = false;
            _laserFire = false;
        }

        private void Fire(InputAction.CallbackContext ctx)
        {
            _fire = true;
        }
        private void LaserFire(InputAction.CallbackContext ctx)
        {
            _laserFire = true;
        }
        
        public override void Destroy()
        {
            _inputActions.Player.Fire.started -= Fire;
            _inputActions.Player.LaserFire.started -= LaserFire;
        }
    }
}
