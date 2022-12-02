using Core;
using UnityEngine;

namespace Asteroids.Movable
{
    public class MovableFactory : AbstractObjectFactory<MovableFacade>
    {
        public override MovableFacade Create(params object[] args)
        {
            var data = (MovableData)args[0];
            var prefab = args[1] as GameObject;
            
            var model = new MovableModel(data);
            var view = GameObject.Instantiate(prefab, data.position, data.rotation)
                .GetComponentInChildren<MovableView>();
            var presenter = new MovablePresenter(model, view);
            
            return new MovableFacade(presenter);
        }
    }
}
