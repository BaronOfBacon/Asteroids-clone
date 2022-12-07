using Asteroids.Movable;
using Asteroids.MovableSystem;
using Asteroids.Player;
using Core;
using UnityEngine;

namespace Asteroids.GameManager
{
    public class GameManagerPresenter : Presenter<GameManagerModel, GameManagerView>
    {
        private MovableSystemFacade _movableSystemFacade;
        private MovableFactory _movableFactory;
        private PlayerFactory _playerFactory;
        private MovableEventsHolder _movableEventsHolder;
        private GameSettings _gameSettings;
        
        public GameManagerPresenter(GameManagerModel model, GameManagerView view) : base(model, view)
        {
            _movableEventsHolder = new MovableEventsHolder();
        }

        public void StartGame(MovableSystemView movableSystemView, GameObject prefab, GameSettings gameSettings)
        {
            _gameSettings = gameSettings;

            var fieldSize = _gameSettings.GameFieldSize;
            var accelerationMultiplier = gameSettings.ForwardAccelerationMultiplier;
            var maxSpeed = gameSettings.MaxSpeed;
            
            var movableSystemFactory = new MovableSystemFactory(_movableEventsHolder);
            _movableSystemFacade = movableSystemFactory.Create(movableSystemView, fieldSize, 
                accelerationMultiplier, maxSpeed);
            
            _movableFactory = new MovableFactory(_movableEventsHolder);

            _playerFactory = new PlayerFactory(_movableFactory);
            
            Debug.Log("Game started!");

            //TODO Init game (spawn player, asteroids)
            
            #region Test

            var movableData = new MovableData()
            {
                mass = 0f,
                acceleration = Vector2.zero /*Vector2.right * 2f*/,
                position = Vector2.zero,
                rotation = Quaternion.FromToRotation(Vector3.up, new Vector2(-0.3f, 0.7f)),
                velocity = Vector2.zero /*new Vector2(-0.3f, 0.7f) * 2f*/,
                friction = gameSettings.PlayerFriction
            };

            IPlayerInputObserver inputObserver = new PlayerInputObserver();
            
            _playerFactory.Create(prefab, movableData, inputObserver, gameSettings.PlayerRotationSpeed);
            
            /*bool isPrefab = true;
            var movable = _movableFactory.Create(
                new MovableData()
                {
                    mass = 0f, 
                    acceleration = Vector2.zero/*Vector2.right * 2f#1#, 
                    position = Vector2.zero, 
                    rotation = Quaternion.FromToRotation(Vector3.up, new Vector2(-0.3f, 0.7f)),
                    velocity = new Vector2(-0.3f,0.7f) * 2f
                }, 
                prefab,
                isPrefab);*/
            
            //movable.Destroy();

            #endregion
        }
    }
}
