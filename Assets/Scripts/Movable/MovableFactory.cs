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
        
        /// <summary>
        /// Creates movable entity.
        /// </summary>
        /// <param name="args">[0] MovableData, [1]Prefab/GameObject (must contain MovableView),
        /// [2]bool (Is [1] - prefab?)</param>
        /// <returns>Returns movable facade.</returns>
        public override MovableFacade Create(params object[] args)
        {
            var data = (MovableData)args[0];
            var gameObject = (GameObject)args[1];
            var isPrefab = (bool)args[2];
            
            var model = new MovableModel(data);
            
            if (isPrefab)
            {
                gameObject = Object.Instantiate(gameObject);
            }

            gameObject.transform.position = data.position;
            gameObject.transform.rotation = data.rotation;

            var view = gameObject.GetComponentInChildren<MovableView>();
            var presenter = new MovablePresenter(model, view);
            var facade = new MovableFacade(presenter);
            
            _movableEventsHolder.AddMovable(facade);
            facade.OnDestroy = _movableEventsHolder.DeleteMovable;
            
            return facade;
        }
        
    }
}
