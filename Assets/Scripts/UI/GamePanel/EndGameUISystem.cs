using System;
using System.Collections.Generic;
using ECS;
using ECS.Messages;

namespace Asteroids.UI.Game
{
    public class EndGameUISystem : ECS.System
    {
        public override IEnumerable<Type> ComponentsMask { get; }

        private EndGamePanelView _endGamePanel;
        
        public EndGameUISystem(EndGamePanelView endGamePanel)
        {
            _endGamePanel = endGamePanel;
            _endGamePanel.RestartButton.onClick.AddListener(RestartGame);
        }

        public override void Initialize(World world, Dispatcher messageDispatcher)
        {
            base.Initialize(world, messageDispatcher);
            MessageDispatcher.Subscribe(MessageType.BestScore, HandleBestScore);
        }

        public override void Process(Entity entity)
        {
        }

        public override void PostProcess()
        {
            
        }

        private void HandleBestScore(object arg)
        {
            _endGamePanel.SetScore((int)arg);
            _endGamePanel.Switch(true);
        }

        private void RestartGame()
        {
            _endGamePanel.Switch(false);
            MessageDispatcher.SendMessage(MessageType.RestartGame, null);
        }
        
        public override void Destroy()
        {
            _endGamePanel.RestartButton.onClick.RemoveListener(RestartGame);
            MessageDispatcher.Unsubscribe(MessageType.PlayerDied, HandleBestScore);
        }
    }
}
