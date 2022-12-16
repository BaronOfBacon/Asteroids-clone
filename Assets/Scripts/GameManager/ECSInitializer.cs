using Asteroids.Asteroid;
using Asteroids.Collisions;
using Asteroids.Helpers;
using Asteroids.Input;
using Asteroids.LaserWeapon;
using Asteroids.Movable;
using Asteroids.Player;
using Asteroids.Weapon.LaserWeapon;
using Asteroids.Weapon.Projectile;
using Asteroids.Weapon.RegularWeapon;
using Data;
using ECS;
using UnityEngine;

namespace Asteroids.GameManager
{
    public class ECSInitializer : MonoBehaviour
    {
        [SerializeField] 
        private GameSettings _gameSettings;
        
        private World _world;
        private InputActions _inputActions;

        private void Awake()
        {
            _world = new World();

            _inputActions = new InputActions();

            var fieldSize = _gameSettings.GameFieldSize;
            var boundariesDistance = new Vector2(fieldSize.x / 2f, fieldSize.y / 2f);
            var fieldCalculationHelper = new FieldCalculationHelper(boundariesDistance);
            _world.AddSystem(new Movable.MovableSystem(_gameSettings.ForwardAccelerationMultiplier,
                _gameSettings.MaxSpeed,fieldCalculationHelper));
            _world.AddSystem(new PlayerInputSystem(_inputActions));
            _world.AddSystem(new PlayerMoveSystem(_gameSettings.PlayerRotationSpeed));
            _world.AddSystem(new RegularWeaponSystem(_world));
            _world.AddSystem(new LaserWeaponSystem(fieldCalculationHelper));
            _world.AddSystem(new PlayerDeathDetectorSystem());
            _world.AddSystem(new ProjectileOnCollisionDestroySystem());
            _world.AddSystem(new AsteroidOnCollisionDestroySystem());
        }

        private void Start()
        {
            InitPlayer();
            InitTestAsteroid();
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
                _gameSettings.LaserChargesCapacity, _gameSettings.LaserChargeCoolDown));

            var triggerDetector = playerGO.GetComponent<TriggerDetector2D>();
            var triggerComponent = (TriggerDetectorComponent)player.AddComponent(new TriggerDetectorComponent());
            triggerComponent.SubscribeDetector(triggerDetector);
        }

        private void InitTestAsteroid()
        {
            var movableInitData = new MovableData()
            {
                acceleration = Vector2.zero,
                position = new Vector2(-3f,0f),
                rotation = Quaternion.FromToRotation(Vector3.up, new Vector2(-0.3f, 0.7f)),
                velocity = Vector2.zero,
                friction = _gameSettings.PlayerFriction
            };
            
            var asteroidGO = GameObject.Instantiate(_gameSettings.AsteroidPrefab, 
                movableInitData.position, movableInitData.rotation);
            var asteroid = _world.CreateEntity(asteroidGO);
            asteroid.AddComponent<AsteroidComponent>();
            
            asteroid.AddComponent(new MovableComponent(movableInitData));

            var triggerDetector = asteroidGO.GetComponent<TriggerDetector2D>();
            var triggerDetectorComponent = (TriggerDetectorComponent)asteroid.AddComponent(new TriggerDetectorComponent());
            triggerDetectorComponent.SubscribeDetector(triggerDetector);
        }
        
        private void Update()
        {
            _world.Update();
        }

        private void OnDestroy()
        {
            _world.Destroy();
        }
    }
}
