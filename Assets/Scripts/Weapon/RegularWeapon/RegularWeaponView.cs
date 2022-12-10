using System;
using Core;
using UnityEngine;

namespace Asteroids.RegularWeapon
{
    public class RegularWeaponView : View
    {
        public EventHandler OnUpdate;
        public Vector2 ProjectileSpawnOffset => _projectileSpawnOffset;
        
        [SerializeField] 
        private Vector2 _projectileSpawnOffset;
        
        #if UNITY_EDITOR
        [SerializeField] 
        private float _sphereRadius;

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position + transform.rotation *_projectileSpawnOffset, _sphereRadius);
        }
        #endif

        private void Update()
        {
            OnUpdate?.Invoke(this,null);
        }
    }
}