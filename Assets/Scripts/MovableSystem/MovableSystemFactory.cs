using Asteroids.Helpers;
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
        /// <param name="args">[0] MovableSystemView, [1] float (Forward acceleration multiplier),
        /// [2] float (Max speed), [3] FieldCalculationHelper</param>
        /// <returns>MovableSystemFacade</returns>
        public override MovableSystemFacade Create(params object[] args)
        {
            var model = new MovableSystemModel();
            var view = (MovableSystemView)args[0];
            var presenter = new MovableSystemPresenter(model, view);

            model.forwardAccelerationMultiplier = (float)args[1];
            model.maxSpeed = (float)args[2];
                
            presenter.Init((FieldCalculationHelper)args[3]);
            
            var facade = new MovableSystemFacade(presenter);
            _movableEventsHolder.MovableCreated += facade.AddMovable;
            _movableEventsHolder.MovableDestroyed += facade.DeleteMovable;
            return facade;
        }
    }
}
