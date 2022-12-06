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
        private GameSettings _gameSettings;
        
        public GameManagerPresenter(GameManagerModel model, GameManagerView view) : base(model, view)
        {
            _movableEventsHolder = new MovableEventsHolder();
        }

        public void StartGame(MovableSystemView movableSystemView, GameObject prefab, GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
            
            var movableSystemFactory = new MovableSystemFactory(_movableEventsHolder);
            _movableSystemFacade = movableSystemFactory.Create(movableSystemView, _gameSettings.GameFieldSize);
            
            _movableFactory = new MovableFactory(_movableEventsHolder);
            
            Debug.Log("Game started!");

            //TODO Init game (spawn player, asteroids)
            
            #region Test
            
            var movable = _movableFactory.Create(
                new MovableData()
                {
                    mass = 0f, acceleration = Vector2.zero/*Vector2.right * 2f*/, position = Vector2.zero, rotation = Quaternion.identity,
                    velocity = new Vector2(-0.3f,0.7f) * 2f
                }, prefab);
            
            //movable.Destroy();

            #endregion
        }
    }
}
