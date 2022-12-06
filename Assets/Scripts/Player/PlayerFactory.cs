using System;
using Asteroids.Movable;
using Core;
using UnityEngine;

namespace Asteroids.Player
{
    public class PlayerFactory : AbstractObjectFactory<PlayerFacade>
    {
        private MovableFactory _movableFactory;

        public PlayerFactory(MovableFactory movableFactory)
        {
            _movableFactory = movableFactory;
        }
        
        /// <summary>
        /// Creates Player entity.
        /// </summary>
        /// <param name="args">[0] GameObject (prefab), [1] MovableData (Init data), [2] IPlayerInputObserver,
        /// [3] float (Player rotation speed)</param>
        /// <returns>PlayerFacade</returns>
        public override PlayerFacade Create(params object[] args)
        {
            var prefab = (GameObject)args[0];
            var movableData = (MovableData)args[1];
            var inputObserver = (IPlayerInputObserver)args[2];
            var playerRotationSpeed = (float)args[3]; 
            
            var gameObject = GameObject.Instantiate(prefab);

            var movableFacade = _movableFactory.Create(movableData, gameObject, false);
            
            var view = gameObject.GetComponentInChildren<PlayerView>();
            var model = new PlayerModel(movableFacade, inputObserver, playerRotationSpeed);
            var presenter = new PlayerPresenter(model, view);
            //sdfdsf
            return new PlayerFacade(presenter);
        }
    }
}
