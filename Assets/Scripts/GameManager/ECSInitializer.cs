using System;
using Asteroids.Asteroid;
using Asteroids.Collisions;
using Asteroids.Helpers;
using Asteroids.Input;
using Asteroids.LaserWeapon;
using Asteroids.Movable;
using Asteroids.Player;
using Asteroids.Score;
using Asteroids.UI.Game;
using Asteroids.Weapon.LaserWeapon;
using Asteroids.Weapon.Projectile;
using Asteroids.Weapon.RegularWeapon;
using Data;
using ECS;
using ECS.Messages;
using UnityEngine;

namespace Asteroids.GameManager
{
    public class ECSInitializer : MonoBehaviour
    {
        [SerializeField] 
        private GameSettings _gameSettings;
        [SerializeField] 
        private AsteroidsSettings _asteroidsSettings;
        [SerializeField]
        private ScoreSettings _scoreSettings;

        [SerializeField] 
        private GameUIPanelView _gameUIPanel;
        
        private World _world;
        private InputActions _inputActions;
        private AsteroidsSpawnSystem _asteroidsSpawnSystem;

        private void Awake()
        {
            Dispatcher messageDispatcher = new Dispatcher();
            
            _world = new World(messageDispatcher);

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
            _world.AddSystem(new GameUISystem());
        }

        private void Start()
        {
            InitPlayer();
            _asteroidsSpawnSystem.Start();
            var score = _world.CreateEntity(null);
            score.AddComponent<ScoreComponent>();
            var gamePanelEntity = _world.CreateEntity(_gameUIPanel.gameObject);
            gamePanelEntity.AddComponent(new GameUIComponent(_gameUIPanel));
        }

        private void InitPlayer()
        {
            var playerGO = GameObject.Instantiate(_gameSettings.PlayerPrefab);
            var player = _world.CreateEntity(playerGO);
            var playerMovableInitData = new MovableData()
            {
                acceleration = Vector2.zero,
                position = Vector2.zero,
                rotation = Quaternion.FromToRotation(Vector3.up, new Vector2(-0.3f, 0.7f)),
                velocity = Vector2.zero,
                friction = _gameSettings.PlayerFriction
            };
            player.AddComponent<PlayerComponent>();
            player.AddComponent(new MovableComponent(playerMovableInitData));
            player.AddComponent(new PlayerInputComponent());
            player.AddComponent(new RegularWeaponComponent(_gameSettings.ProjectilePrefab,
                _gameSettings.ProjectileSpeed, _gameSettings.ProjectileSpawnOffset));

            var laserView = playerGO.GetComponent<LaserWeaponView>();
            player.AddComponent(new LaserWeaponComponent(laserView, _gameSettings.LaserActiveTimeDuration,
                _gameSettings.LaserChargesCapacity, _gameSettings.LaserChargeCooldown));

            var collisionDetector = playerGO.GetComponent<CollisionDetector2D>();
            var collisionDetectorComponent = (CollisionDetectorComponent)player.AddComponent(new CollisionDetectorComponent());
            collisionDetectorComponent.SubscribeDetector(collisionDetector);
        }

        private void Update()
        {
            _world.Process();
        }

        private void LateUpdate()
        {
            _world.LateUpdate();
        }

        private void OnDestroy()
        {
            _world.Destroy();
        }
    }
}
