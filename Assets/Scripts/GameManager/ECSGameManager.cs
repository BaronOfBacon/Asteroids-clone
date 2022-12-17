using Asteroids.Asteroid;
using Asteroids.Helpers;
using Asteroids.Input;
using Asteroids.Movable;
using Asteroids.Player;
using Asteroids.Score;
using Asteroids.UFO;
using Asteroids.UI.Game;
using Asteroids.Weapon.LaserWeapon;
using Asteroids.Weapon.Projectile;
using Asteroids.Weapon.RegularWeapon;
using ECS;
using ECS.Messages;
using UnityEngine;

namespace Asteroids.GameManager
{
    public class ECSGameManager : MonoBehaviour
    {
        [SerializeField] 
        private GameSettings _gameSettings;
        [SerializeField] 
        private AsteroidsSettings _asteroidsSettings;
        [SerializeField]
        private ScoreSettings _scoreSettings;
        [SerializeField] 
        private PursuitPlayerSettings _pursuitPlayerSettings;
        [SerializeField] 
        private PlayerSettings _playerSettings;
        [SerializeField] 
        private RegularWeaponSettings _regularWeaponSettings;
        [SerializeField] 
        private LaserWeaponSettings _laserWeaponSettings;
        
        [SerializeField] 
        private GameUIPanelView _gameUIPanel;
        [SerializeField] 
        private EndGamePanelView _endGameUIPanel;
        
        private World _world;
        private InputActions _inputActions;
        private AsteroidsSpawnSystem _asteroidsSpawnSystem;
        private PursuitPlayerSystem _pursuitPlayerSystem;
        private Dispatcher _messageDispatcher;
        
        private void Awake()
        {
            _messageDispatcher = new Dispatcher();
            _world = new World(_messageDispatcher);
            _inputActions = new InputActions();

            InitSystems();

            _messageDispatcher.Subscribe(MessageType.RestartGame, Restart);
        }

        private void InitSystems()
        {
            var fieldSize = _gameSettings.GameFieldSize;
            var boundariesDistance = new Vector2(fieldSize.x / 2f, fieldSize.y / 2f);
            var fieldCalculationHelper = new FieldCalculationHelper(boundariesDistance);
            
            _world.AddSystem(new MovableSystem(_playerSettings.ForwardAccelerationMultiplier,
                _gameSettings.MaxSpeed,fieldCalculationHelper));
            _world.AddSystem(new PlayerInputSystem(_inputActions));
            _world.AddSystem(new PlayerMoveSystem(_playerSettings.PlayerRotationSpeed));
            _world.AddSystem(new RegularWeaponSystem(_world));
            _world.AddSystem(new ProjectilesDestroyerSystem());
            _world.AddSystem(new LaserWeaponSystem(fieldCalculationHelper));
            _world.AddSystem(new PlayerDeathDetectorSystem());
            _world.AddSystem(new ProjectileOnCollisionDestroySystem());
            _world.AddSystem(new AsteroidOnCollisionDestroySystem());
            _asteroidsSpawnSystem = new AsteroidsSpawnSystem(_asteroidsSettings, fieldCalculationHelper);
            _world.AddSystem(_asteroidsSpawnSystem);
            _world.AddSystem(new ScoreSystem(_scoreSettings));
            _world.AddSystem(new PlayerSpatialDataNotifierSystem());
            _world.AddSystem(new GameUISystem(_gameUIPanel));
            _world.AddSystem(new PlayerLaserUIDataNotifierSystem());
            _world.AddSystem(new EndGameUISystem(_endGameUIPanel));
            _pursuitPlayerSystem = new PursuitPlayerSystem(fieldCalculationHelper, _pursuitPlayerSettings);
            _world.AddSystem(_pursuitPlayerSystem);
            _world.AddSystem(new PursuerOnCollisionDestroySystem());
            _world.AddSystem(new PlayerSpawnSystem(_playerSettings, _regularWeaponSettings, _laserWeaponSettings));
        }
        
        private void Start()
        {
            _messageDispatcher.SendMessage(MessageType.SpawnPlayer, null);
            
            var score = _world.CreateEntity(null);
            score.AddComponent<ScoreComponent>();
            var gamePanelEntity = _world.CreateEntity(_gameUIPanel.gameObject);
            gamePanelEntity.AddComponent(new GameUIComponent());
        }

        private void Update()
        {
            _world.Process();
        }

        private void LateUpdate()
        {
            _world.LateUpdate();
        }

        private void Restart(object arg)
        {
            _messageDispatcher.SendMessage(MessageType.SpawnPlayer, null);
        }

        private void OnDestroy()
        {
            _messageDispatcher.Unsubscribe(MessageType.RestartGame, Restart);
            _world.Destroy();
        }
    }
}
