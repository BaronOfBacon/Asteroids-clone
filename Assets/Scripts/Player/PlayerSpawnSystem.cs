using System;
using System.Collections.Generic;
using Asteroids.Collisions;
using Asteroids.Input;
using Asteroids.LaserWeapon;
using Asteroids.Movable;
using Asteroids.Weapon.LaserWeapon;
using Asteroids.Weapon.RegularWeapon;
using ECS;
using ECS.Messages;
using UnityEngine;

namespace Asteroids.Player
{
    public class PlayerSpawnSystem : ECS.System
    {
        public override IEnumerable<Type> ComponentsMask { get; }

        private PlayerSettings _playerSettings;
        private RegularWeaponSettings _regularWeaponSettings;
        private LaserWeaponSettings _laserWeaponSettings;
        private Entity _player;

        
        public PlayerSpawnSystem(PlayerSettings playerSettings, RegularWeaponSettings regularWeaponSettings,
            LaserWeaponSettings laserWeaponSettings)
        {
            _playerSettings = playerSettings;
            _regularWeaponSettings = regularWeaponSettings;
            _laserWeaponSettings = laserWeaponSettings;
        }
        
        public override void Initialize(World world, Dispatcher messageDispatcher)
        {
            base.Initialize(world, messageDispatcher);
            MessageDispatcher.Subscribe(MessageType.PlayerDied, HandlePlayerDeath);
            MessageDispatcher.Subscribe(MessageType.SpawnPlayer, SpawnPlayer);
        }

        public override void Process(Entity entity)
        {
        }

        public override void PostProcess()
        {
        }

        public void HandlePlayerDeath(object arg)
        {
            _player?.InitDestroy();
            _player = null;
        }

        public void SpawnPlayer(object arg)
        {
            var playerGO = GameObject.Instantiate(_playerSettings.PlayerPrefab);
            
            _player = World.CreateEntity(playerGO);
            var playerMovableInitData = new MovableData()
            {
                acceleration = Vector2.zero,
                position = Vector2.zero,
                rotation = Quaternion.FromToRotation(Vector3.up, new Vector2(-0.3f, 0.7f)),
                velocity = Vector2.zero,
                friction = _playerSettings.PlayerFriction
            };
            _player.AddComponent<PlayerComponent>();
            _player.AddComponent(new MovableComponent(playerMovableInitData));
            _player.AddComponent(new PlayerInputComponent());
            _player.AddComponent(new RegularWeaponComponent(_regularWeaponSettings.ProjectilePrefab,
                _regularWeaponSettings.ProjectileSpeed, _regularWeaponSettings.ProjectileSpawnOffset));

            var laserView = playerGO.GetComponent<LaserWeaponView>();
            _player.AddComponent(new LaserWeaponComponent(laserView, _laserWeaponSettings.LaserActiveTimeDuration,
                _laserWeaponSettings.LaserChargesCapacity, _laserWeaponSettings.LaserChargeCooldown));

            var collisionDetector = playerGO.GetComponent<CollisionDetector2D>();
            var collisionDetectorComponent = (CollisionDetectorComponent)_player.AddComponent(new CollisionDetectorComponent());
            collisionDetectorComponent.SubscribeDetector(collisionDetector);

            MessageDispatcher.SendMessage(MessageType.PlayerSpawned, _player);
            
        }

        public override void Destroy()
        {
            MessageDispatcher.Unsubscribe(MessageType.PlayerDied, HandlePlayerDeath);
            MessageDispatcher.Unsubscribe(MessageType.SpawnPlayer, SpawnPlayer);
        }
    }
}
