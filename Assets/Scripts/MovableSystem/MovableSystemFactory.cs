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
        public override MovableSystemFacade Create(params object[] args)
        {
            var model = new MovableSystemModel();
            var view = (MovableSystemView)args[0];
            var presenter = new MovableSystemPresenter(model, view);
            presenter.Init((Vector2)args[1]);
            var facade = new MovableSystemFacade(presenter);
            _movableEventsHolder.MovableCreated += facade.AddMovable;
            _movableEventsHolder.MovableDestroyed += facade.DeleteMovable;
            return facade;
        }
    }
}
