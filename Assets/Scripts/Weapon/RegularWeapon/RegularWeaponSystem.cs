using System;
using System.Collections.Generic;
using Asteroids.Collisions;
using Asteroids.Input;
using Asteroids.Movable;
using Asteroids.Weapon.Projectile;
using ECS;
using UnityEngine;

namespace Asteroids.Weapon.RegularWeapon
{
    public class RegularWeaponSystem : ECS.System
    {
        public override IEnumerable<Type> ComponentsMask => 
            new List<Type>() {typeof(RegularWeaponComponent), typeof(PlayerInputComponent)};
        
        private World _world;
        
        public RegularWeaponSystem(World world)
        {
            _world = world;
        }
            
        public override void Process(Entity entity)
        {
            var inputComponent = entity.GetComponent<PlayerInputComponent>();

            if (!inputComponent.Fire) return;
            
            var weaponComponent = entity.GetComponent<RegularWeaponComponent>();
            
            var rootTransform = entity.GameObject.transform;

            var movableData = new MovableData()
            {
                position = rootTransform.position +
                           rootTransform.rotation * weaponComponent.ProjectileSpawnOffset,
                rotation = Quaternion.identity,
                velocity = rootTransform.up * weaponComponent.ProjectileSpeed,
                destroyOutsideTheField = true
            };

            var projectileGO = GameObject.Instantiate(weaponComponent.ProjectilePrefab, movableData.position,
                movableData.rotation);
            
            var projectileEntity = _world.CreateEntity(projectileGO);
            
            projectileEntity.AddComponent(new MovableComponent(movableData));
            var collisionDetector = projectileGO.GetComponent<CollisionDetector2D>();
            var collisionDetectorComponent = (CollisionDetectorComponent) projectileEntity.AddComponent(new CollisionDetectorComponent());
            collisionDetectorComponent.SubscribeDetector(collisionDetector);
            projectileEntity.AddComponent(new ProjectileComponent());
        }

        public override void PostProcess()
        {
            
        }

        public override void Destroy()
        {
            //_inputActions.Player.Fire.started -= HandleFire;
        }
    }
}
