using Asteroids.Movable;
using Core;
using UnityEngine;

namespace Asteroids.MovableSystem
{
    public class MovableSystemFactory : AbstractObjectFactory<MovableSystemFacade>
    {
        private MovableEventsHolder _movableEventsHolder;

        public MovableSystemFactory(MovableEventsHolder movableEventsHolder)
        {
            _movableEventsHolder = movableEventsHolder;
        }
        
        /// <summary>
        /// Creates movable system entity.
        /// </summary>
        /// <param name="args">[0] MovableSystemView, [1] Vector2 (Distance to boundaries of game field),
        /// [2] float (Forward acceleration multiplier), [3] float (Max speed)</param>
        /// <returns>MovableSystemFacade</returns>
        public override MovableSystemFacade Create(params object[] args)
        {
            var model = new MovableSystemModel();
            var view = (MovableSystemView)args[0];
            var presenter = new MovableSystemPresenter(model, view);
            var gameFieldSize = (Vector2)args[1];
            
            model.fieldBoundariesDistance = new Vector2(gameFieldSize.x / 2f, gameFieldSize.y / 2f);
            model.forwardAccelerationMultiplier = (float)args[2];
            model.maxSpeed = (float)args[3];
                
            presenter.Init();
            
            var facade = new MovableSystemFacade(presenter);
            _movableEventsHolder.MovableCreated += facade.AddMovable;
            _movableEventsHolder.MovableDestroyed += facade.DeleteMovable;
            return facade;
        }
    }
}
