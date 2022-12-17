using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Weapon.RegularWeapon
{
    [CreateAssetMenu(fileName = "RegularWeaponSetting", menuName = "Data/RegularWeaponSettings")]
    public class RegularWeaponSettings : ScriptableObject
    {
        public GameObject ProjectilePrefab => _projectilePrefab;
        public float ProjectileSpeed => _projectileSpeed;
        public Vector2 ProjectileSpawnOffset => _projectileSpawnOffset;
        
        [SerializeField]
        private GameObject _projectilePrefab;
        [SerializeField] 
        private float _projectileSpeed = 1f;
        [SerializeField] 
        private Vector2 _projectileSpawnOffset = new Vector2(0f, 0.5f);
    }
}
