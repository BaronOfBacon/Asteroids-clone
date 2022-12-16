using UnityEngine;
using Component = ECS.Component;

namespace Asteroids.Weapon.RegularWeapon
{
    public class RegularWeaponComponent : Component
    {
        public GameObject ProjectilePrefab { get; private set; }
        public float ProjectileSpeed { get; private set; }
        public Vector3 ProjectileSpawnOffset { get; private set; }

        public RegularWeaponComponent(GameObject projectilePrefab, float projectileSpeed, Vector3 projectileSpawnOffset)
        {
            ProjectilePrefab = projectilePrefab;
            ProjectileSpeed = projectileSpeed;
            ProjectileSpawnOffset = projectileSpawnOffset;
        }
    }
}
