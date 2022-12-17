using System;
using System.Collections.Generic;
using System.Linq;
using Asteroids.Collisions;
using Asteroids.Helpers;
using Asteroids.Movable;
using ECS;
using ECS.Messages;
using UnityEngine;

namespace Asteroids.UFO
{
    public class PursuitPlayerSystem : ECS.System
    {
        private const float OVERLAP_RADIUS = 1.5f;

        public override IEnumerable<Type> ComponentsMask => _componentsMask;
        
        private IEnumerable<Type> _componentsMask = new List<Type>()
        {
            typeof(PlayerPursuerComponent),
            typeof(MovableComponent),
        };
        
        private Dictionary<PlayerPursuerComponent, Entity> _pursuers = new Dictionary<PlayerPursuerComponent, Entity>();
        private GameObject _target;
        private float _spawnTimeLeft;
        private float _spawnCooldown;
        private GameObject _pursuerPrefab;
        private float _pursuerSpeed;
        private FieldCalculationHelper _fieldCalculationHelper;
        private Collider2D[] _buffer;
        private bool _enabled = true;
        
        public PursuitPlayerSystem(FieldCalculationHelper fieldCalculationHelper, 
            PursuitPlayerSettings pursuitPlayerSettings)
        {
            _fieldCalculationHelper = fieldCalculationHelper;
            _buffer = new Collider2D[100];

            _spawnCooldown = pursuitPlayerSettings.SpawnCooldown;
            _spawnTimeLeft = pursuitPlayerSettings.SpawnCooldown;
            _pursuerPrefab = pursuitPlayerSettings.PursuerPrefab;
            _pursuerSpeed = pursuitPlayerSettings.PursuerSpeed;
        }
        
        public override void Initialize(World world, Dispatcher messageDispatcher)
        {
            base.Initialize(world, messageDispatcher);
            MessageDispatcher.Subscribe(MessageType.PlayerDied, HandlePlayerDied);
            MessageDispatcher.Subscribe(MessageType.PlayerPursuerKilled, HandlePursuerKilled);
            MessageDispatcher.Subscribe(MessageType.RestartGame, Restart);
            MessageDispatcher.Subscribe(MessageType.PlayerSpawned, HandlePlayerSpawned);
        }
        
        public override void Process(Entity entity)
        {
            var movableComponent = entity.GetComponent<MovableComponent>();
            var directionToTarget = _target.transform.position - entity.GameObject.transform.position;
            movableComponent.Velocity = _pursuerSpeed * directionToTarget.normalized;
        }

        private Vector3 GetAsteroidRandomSpawnPosition()
        {
            Vector3 position;
            do
            {
                position = _fieldCalculationHelper.GetRandomPositionOnBorders();
                
            } while (Physics2D.OverlapCircleNonAlloc(position, OVERLAP_RADIUS, _buffer) != 0);

            return position;
        }
        
        private void SpawnPursuerOnRandomPosition()
        {
            Vector2 spawnPosition = GetAsteroidRandomSpawnPosition();

            var movableData = new MovableData()
            {
                accelerateForward = false,
                acceleration = Vector2.zero,
                destroyOutsideTheField = false,
                velocity = Vector2.zero,
                friction = 0f,
                position = spawnPosition,
                rotation = Quaternion.identity
            };
            
            var pursuerGO = GameObject.Instantiate(_pursuerPrefab,movableData.position,
                movableData.rotation);
            var pursuer = World.CreateEntity(pursuerGO);
            pursuer.AddComponent(new MovableComponent(movableData));
            var pursuerComponent = new PlayerPursuerComponent();
            pursuer.AddComponent(pursuerComponent);
            var collisionDetector = pursuerGO.GetComponent<CollisionDetector2D>();
            var collisionDetectorComponent =
                (CollisionDetectorComponent)pursuer.AddComponent(new CollisionDetectorComponent());
            collisionDetectorComponent.SubscribeDetector(collisionDetector);
            
            _pursuers.Add(pursuerComponent, pursuer);
        }

        public override void PostProcess()
        {
            if (!_enabled) return;
            
            if (_spawnTimeLeft > 0)
            {
                _spawnTimeLeft -= Time.deltaTime;
            }
            else
            {
                SpawnPursuerOnRandomPosition();
                _spawnTimeLeft = _spawnCooldown;
            }
        }

        private void HandlePlayerDied(object arg)
        {
            DestroyAllPursuers();
            _enabled = false;
        }
        private void Restart(object arg)
        {
            _enabled = true;
            _spawnTimeLeft = _spawnCooldown;
        }
        
        private void HandlePursuerKilled(object arg)
        {
            Entity pursuerEntity = (Entity)arg;
            DestroyPursuer(pursuerEntity.GetComponent<PlayerPursuerComponent>());
        }

        private void DestroyAllPursuers()
        {
            while (_pursuers.Any())
            {
                var pair = _pursuers.First();
                DestroyPursuer(pair.Key);
            }
        }

        private void DestroyPursuer(PlayerPursuerComponent pursuer)
        {
            _pursuers.TryGetValue(pursuer, out var entity);
            _pursuers.Remove(pursuer);
            entity?.InitDestroy();
        }

        private void HandlePlayerSpawned(object player)
        {
            var playerEntity = (Entity)player;
            _target = playerEntity.GameObject;
        }

        public override void Destroy()
        {
            MessageDispatcher.Unsubscribe(MessageType.PlayerDied, HandlePlayerDied);
            MessageDispatcher.Unsubscribe(MessageType.PlayerPursuerKilled, HandlePursuerKilled);
            MessageDispatcher.Unsubscribe(MessageType.RestartGame, Restart);
            MessageDispatcher.Unsubscribe(MessageType.PlayerSpawned, HandlePlayerSpawned);
        }
    }
}
