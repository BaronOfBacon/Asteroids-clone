using System;
using Asteroids.Movable;
using Core;
using UnityEngine;

namespace Asteroids.Weapon.Projectile
{
    public class ProjectileFactory : AbstractObjectFactory<ProjectileFacade>
    {
        private GameObject _prefab;
        private MovableFactory _movableFactory;
        
        public ProjectileFactory(GameObject prefab, MovableFactory movableFactory)
        {
            _prefab = prefab;
            _movableFactory = movableFactory;
        }
        
        /// <summary>
        /// Creates projectile entity.
        /// </summary>
        /// <param name="args">[0] Vector2 (Position), [1] Vector2 (Velocity)</param>
        /// <returns>Returns ProjectileFacade.</returns>
        public override ProjectileFacade Create(params object[] args)
        {
            var movableData = new MovableData()
            {
                position = (Vector3)args[0],
                accelerateForward = false,
                acceleration = Vector2.zero,
                friction = 0f,
                mass = 1f,
                velocity = (Vector3)args[1],
                destroyOutsideTheField = true
            };
            var projectileGO = GameObject.Instantiate(_prefab);
            var view = projectileGO.GetComponentInChildren<ProjectileView>();
            var movable = _movableFactory.Create(movableData, projectileGO, false);
            var model = new ProjectileModel(movable);
            var presenter = new ProjectilePresenter(model, view);

            return new ProjectileFacade(presenter);
        }
    }
}
