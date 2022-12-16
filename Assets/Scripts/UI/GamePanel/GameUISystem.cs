using System;
using System.Collections.Generic;
using ECS;
using ECS.Messages;
using UnityEngine;

namespace Asteroids.UI.Game
{
    public class GameUISystem : ECS.System
    {
        public override IEnumerable<Type> ComponentsMask => _componentsMask;
        
        private IEnumerable<Type> _componentsMask;

        private GameUIPanelView _gameUIPanel;
        private Vector2 _position;
        private float _angle;
        private Vector2 _velocity;
        private int _charges;
        private float _cooldown;
        private bool _hidden;
        
        public GameUISystem(GameUIPanelView panel)
        {
            _componentsMask = new List<Type>()
            {
                typeof(GameUIComponent)
            };
            _gameUIPanel = panel;
        }

        public override void Initialize(World world, Dispatcher messageDispatcher)
        {
            base.Initialize(world, messageDispatcher);
            SubscribeDataListening();
            MessageDispatcher.Subscribe(MessageType.PlayerDied, HidePanel);
            MessageDispatcher.Subscribe(MessageType.RestartGame, ShowPanel);
        }

        public override void Process(Entity entity)
        {
            var component = entity.GetComponent<GameUIComponent>();
            
            if (component.Position != _position)
            {
                component.Position = _position;
                _gameUIPanel.UpdateCoordinates(_position);
            }
            if (component.Angle != _angle)
            {
                component.Angle = _angle;
                _gameUIPanel.UpdateAngle(_angle);
            }
            if (component.Velocity != _velocity)
            {
                component.Velocity = _velocity;
                _gameUIPanel.UpdateSpeed(_velocity);
            }
            if (component.Charges != _charges)
            {
                component.Charges = _charges;
                _gameUIPanel.UpdateLaserCharges(_charges);
            }
            if (component.Cooldown != _cooldown)
            {
                component.Cooldown = _cooldown;
                _gameUIPanel.UpdateLaserChargesCooldown(_cooldown);
            }
        }

        public override void PostProcess()
        {
        }

        private void HandleSpatialDataChange(object spatialData)
        {
            var data = (Tuple<Vector2, float, Vector2>)spatialData;
            _position = data.Item1;
            _angle = data.Item2;
            _velocity = data.Item3;
        }
        
        private void HandleLaserDataChange(object laserData)
        {
           var data = (Tuple<int, float>)laserData;
           _charges = data.Item1;
           _cooldown = data.Item2;
        }
        
        private void HidePanel(object arg)
        {
            _hidden = true;
            _gameUIPanel.gameObject.SetActive(false);
            UnsubscribeDataListening();
        }
        
        private void ShowPanel(object arg)
        {
            _hidden = false;
            _gameUIPanel.gameObject.SetActive(true);
            SubscribeDataListening();
        }

        private void SubscribeDataListening()
        {
            MessageDispatcher.Subscribe(MessageType.PlayerSpatialDataChanged, HandleSpatialDataChange);
            MessageDispatcher.Subscribe(MessageType.LaserDataChanged, HandleLaserDataChange);
        }
        
        private void UnsubscribeDataListening()
        {
            MessageDispatcher.Unsubscribe(MessageType.PlayerSpatialDataChanged, HandleSpatialDataChange);
            MessageDispatcher.Unsubscribe(MessageType.LaserDataChanged, HandleLaserDataChange);
        }
        
        public override void Destroy()
        {
            UnsubscribeDataListening();
            MessageDispatcher.Unsubscribe(MessageType.PlayerDied, HidePanel);
            MessageDispatcher.Unsubscribe(MessageType.RestartGame, ShowPanel);
        }
    }
}
