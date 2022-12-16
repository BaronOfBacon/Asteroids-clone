using System;
using System.Collections.Generic;
using Asteroids.Asteroid;
using ECS;
using ECS.Messages;

namespace Asteroids.Score
{
    public class ScoreSystem : ECS.System
    {
        public override IEnumerable<Type> ComponentsMask => _componentsMask;

        private IEnumerable<Type> _componentsMask;
        
        private int _score;
        private ScoreSettings _settings;
        
        public ScoreSystem(ScoreSettings settings)
        {
            _componentsMask = new List<Type>()
            {
                typeof(ScoreComponent)
            };
            
            _settings = settings;
        }

        public override void Initialize(World world, Dispatcher messageDispatcher)
        {
            base.Initialize(world, messageDispatcher);
            MessageDispatcher.Subscribe(MessageType.AsteroidKilled, HandleAsteroidKilled);
            MessageDispatcher.Subscribe(MessageType.PlayerDied, HandlePlayerDied);
        }

        public override void Process(Entity entity)
        {
            entity.GetComponent<ScoreComponent>().Score = _score;
        }

        public override void PostProcess()
        {
            
        }

        private void HandleAsteroidKilled(object asteroid)
        {
            var asteroidComponent = (AsteroidComponent) asteroid;
            var pointForKill = asteroidComponent.IsFraction ? _settings.AsteroidFractionKillPoints : 
                _settings.AsteroidKillPoints;
            _score += pointForKill;
        }
        
        private void HandlePlayerDied(object arg)
        {
            MessageDispatcher.SendMessage(MessageType.BestScore, _score);
            _score = 0;
        }

        public override void Destroy()
        {
            MessageDispatcher.Unsubscribe(MessageType.AsteroidKilled, HandleAsteroidKilled);
            MessageDispatcher.Unsubscribe(MessageType.PlayerDied, HandlePlayerDied);
        }
    }
}
