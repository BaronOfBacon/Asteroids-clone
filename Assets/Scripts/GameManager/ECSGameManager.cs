using Asteroids.Asteroid;
using Asteroids.Collisions;
using Asteroids.Helpers;
using Asteroids.Input;
using Asteroids.LaserWeapon;
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
        private GameUIPanelView _gameUIPanel;
        [SerializeField] 
        private EndGamePanelView _endGameUIPanel;
        
        private World _world;
        private InputActions _inputActions;
        private AsteroidsSpawnSystem _asteroidsSpawnSystem;
        private PursuitPlayerSystem _pursuitPlayerSystem;
        private Dispatcher _messageDispatcher;
        private Entity _player;
        
        private void Awake()
        {
            _messageDispatcher = new Dispatcher();
            _world = new World(_messageDispatcher);
            _inputActions = new InputActions();

            var fieldSize = _gameSettings.GameFieldSize;
            var boundariesDistance = new Vector2(fieldSize.x / 2f, fieldSize.y / 2f);
            var fieldCalculationHelper = new FieldCalculationHelper(boundariesDistance);
            _world.AddSystem(new MovableSystem(_gameSettings.ForwardAccelerationMultiplier,
                _gameSettings.MaxSpeed,fieldCalculationHelper));
            _world.AddSystem(new PlayerInputSystem(_inputActions));
            _world.AddSystem(new PlayerMoveSystem(_gameSettings.PlayerRotationSpeed));
            _world.AddSystem(new RegularWeaponSystem(_world));
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

            _messageDispatcher.Subscribe(MessageType.PlayerDied, HandlePlayerDeath);
            _messageDispatcher.Subscribe(MessageType.RestartGame, Restart);
        }

        private void Start()
        {
            InitPlayer();
            _asteroidsSpawnSystem.Start();
            var score = _world.CreateEntity(null);
            score.AddComponent<ScoreComponent>();
            var gamePanelEntity = _world.CreateEntity(_gameUIPanel.gameObject);
            gamePanelEntity.AddComponent(new GameUIComponent());
        }

        private void InitPlayer()
        {
            var playerGO = GameObject.Instantiate(_gameSettings.PlayerPrefab);
            _asteroidsSpawnSystem.SetSpawnAvoidableObject(playerGO);
            _player = _world.CreateEntity(playerGO);
            var playerMovableInitData = new MovableData()
            {
                acceleration = Vector2.zero,
                position = Vector2.zero,
                rotation = Quaternion.FromToRotation(Vector3.up, new Vector2(-0.3f, 0.7f)),
                velocity = Vector2.zero,
                friction = _gameSettings.PlayerFriction
            };
            _player.AddComponent<PlayerComponent>();
            _player.AddComponent(new MovableComponent(playerMovableInitData));
            _player.AddComponent(new PlayerInputComponent());
            _player.AddComponent(new RegularWeaponComponent(_gameSettings.ProjectilePrefab,
                _gameSettings.ProjectileSpeed, _gameSettings.ProjectileSpawnOffset));

            var laserView = playerGO.GetComponent<LaserWeaponView>();
            _player.AddComponent(new LaserWeaponComponent(laserView, _gameSettings.LaserActiveTimeDuration,
                _gameSettings.LaserChargesCapacity, _gameSettings.LaserChargeCooldown));

            var collisionDetector = playerGO.GetComponent<CollisionDetector2D>();
            var collisionDetectorComponent = (CollisionDetectorComponent)_player.AddComponent(new CollisionDetectorComponent());
            collisionDetectorComponent.SubscribeDetector(collisionDetector);
            
            _pursuitPlayerSystem.SetTarget(playerGO);
        }

        private void Update()
        {
            _world.Process();
        }

        private void LateUpdate()
        {
            _world.LateUpdate();
        }
        
        private void HandlePlayerDeath(object arg)
        {
            _player.InitDestroy();
        }
        
        private void Restart(object arg)
        {
            InitPlayer();
        }

        private void OnDestroy()
        {
            _messageDispatcher.Unsubscribe(MessageType.PlayerDied, HandlePlayerDeath);
            _messageDispatcher.Subscribe(MessageType.RestartGame, Restart);
            _world.Destroy();
        }
    }
}
