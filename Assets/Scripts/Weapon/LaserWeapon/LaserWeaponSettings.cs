using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Weapon.LaserWeapon
{
    [CreateAssetMenu(fileName = "LaserWeaponSettings", menuName = "Data/LaserWeaponSettings")]
    public class LaserWeaponSettings : ScriptableObject
    {
        public int LaserChargesCapacity => _laserChargesCapacity;
        public float LaserActiveTimeDuration => _laserActiveTimeDuration;
        public float LaserChargeCooldown => laserChargeCooldown;
        
        [SerializeField] 
        private int _laserChargesCapacity = 4;
        [SerializeField] 
        private float _laserActiveTimeDuration = 0.5f;
        [SerializeField] 
        private float laserChargeCooldown = 2f;
    }
}
