using System;
using Asteroids.Movable;
using Asteroids.MovableSystem;
using Core;
using UnityEngine;

namespace Asteroids.GameManager
{
    public class GameManagerPresenter : Presenter<GameManagerModel, GameManagerView>
    {
        private MovableSystemFacade _movableSystemFacade;
        private MovableFactory _movableFactory;
        private MovableEventsHolder _movableEventsHolder;
        
        public GameManagerPresenter(GameManagerModel model, GameManagerView view) : base(model, view)
        {
            _movableEventsHolder = new MovableEventsHolder();
        }

        public void StartGame(MovableSystemView movableSystemView, GameObject prefab)
        {
            var movableSystemFactory = new MovableSystemFactory(_movableEventsHolder);
            _movableSystemFacade = movableSystemFactory.Create(movableSystemView);
            
            _movableFactory = new MovableFactory(_movableEventsHolder);
            
            Debug.Log("Game started!");

            #region Test
            var movable = _movableFactory.Create(
                new MovableData()
                {
                    mass = 0f, acceleration = Vector2.zero, position = Vector2.zero, rotation = Quaternion.identity,
                    velocity = Vector2.up * 3f
                }, prefab);
            
            //movable.Destroy();
            #endregion
        }
    }
}
