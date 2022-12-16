using System;
using System.Collections.Generic;
using Asteroids.Input;
using ECS;
using ECS.Messages;
using UnityEngine.InputSystem;

namespace Asteroids.Player
{
    public class PlayerInputSystem : ECS.System
    {
        public override IEnumerable<Type> ComponentsMask => _componentsMask;

        private IEnumerable<Type> _componentsMask = new List<Type>()
        {
            typeof(PlayerInputComponent)
        };
        
        private InputActions _inputActions;
        private float _rotationSpeed;
        private bool _fire;
        private bool _laserFire;
        private bool _paused;
        
        public PlayerInputSystem(InputActions inputActions)
        {
            _inputActions = inputActions;
            _inputActions.Player.Enable();
            _inputActions.Player.Fire.started += Fire;
            _inputActions.Player.LaserFire.started += LaserFire;
        }

        public override void Initialize(World world, Dispatcher messageDispatcher)
        {
            base.Initialize(world, messageDispatcher);
            MessageDispatcher.Subscribe(MessageType.PlayerDied, Pause);
            MessageDispatcher.Subscribe(MessageType.RestartGame, Unpause);
        }

        public override void Process(Entity entity)
        {
            if (_paused) return;
            
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

        private void Pause(object args)
        {
            _inputActions.Player.Disable();
            _paused = true;
        }
        
        private void Unpause(object args)
        {
            _inputActions.Player.Enable();
            _paused = false;
        }
        
        public override void Destroy()
        {
            MessageDispatcher.Unsubscribe(MessageType.PlayerDied, Pause);
            MessageDispatcher.Unsubscribe(MessageType.RestartGame, Unpause);
            _inputActions.Player.Fire.started -= Fire;
            _inputActions.Player.LaserFire.started -= LaserFire;
        }
    }
}
