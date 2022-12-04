using Core;
using UnityEngine;

namespace Asteroids.Movable
{
    public class MovableFactory : AbstractObjectFactory<MovableFacade>
    {
        private MovableEventsHolder _movableEventsHolder;

        public MovableFactory(MovableEventsHolder holder)
        {
            _movableEventsHolder = holder;
        }
        
        public override MovableFacade Create(params object[] args)
        {
            var data = (MovableData)args[0];
            var prefab = args[1] as GameObject;
            
            var model = new MovableModel(data);
            var view = GameObject.Instantiate(prefab, data.position, data.rotation)
                .GetComponentInChildren<MovableView>();
            var presenter = new MovablePresenter(model, view);
            var facade = new MovableFacade(presenter);
            
            _movableEventsHolder.AddMovable(facade);
            facade.OnDestroy = _movableEventsHolder.DeleteMovable;
            
            return facade;
        }
        
    }
}
