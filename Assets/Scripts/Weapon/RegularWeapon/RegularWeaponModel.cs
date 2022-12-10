using Asteroids.Weapon.Projectile;
using Core;
using UnityEngine;

namespace Asteroids.RegularWeapon
{
    public class RegularWeaponModel : Model
    {
        public Vector2 ProjectileSpawnOffset { get; private set; }
        public GameObject ProjectilePrefab { get; private set; }
        public float ProjectileSpeed { get; private set; }
        public ProjectileFactory ProjectileFactory { get; private set; }

        public RegularWeaponModel(Vector2 projectileSpawnOffset, GameObject projectilePrefab, float projectileSpeed, 
            ProjectileFactory projectileFactory)
        {
            ProjectileSpawnOffset = projectileSpawnOffset;
            ProjectilePrefab = projectilePrefab;
            ProjectileSpeed = projectileSpeed;
            ProjectileFactory = projectileFactory;
        }
    }
}
