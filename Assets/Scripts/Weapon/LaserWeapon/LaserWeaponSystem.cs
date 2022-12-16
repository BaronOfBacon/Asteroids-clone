using System;
using System.Collections.Generic;
using Asteroids.Helpers;
using Asteroids.Input;
using Asteroids.Movable;
using ECS;
using UnityEngine;

namespace Asteroids.Weapon.LaserWeapon
{
    public class LaserWeaponSystem : ECS.System
    {
        public override IEnumerable<Type> ComponentsMask
        {
            get => new List<Type>()
            {
                typeof(MovableComponent),
                typeof(LaserWeaponComponent),
                typeof(PlayerInputComponent)
            };
        }

        private FieldCalculationHelper _fieldCalculationHelper;

        public LaserWeaponSystem(FieldCalculationHelper fieldCalculationHelper)
        {
            _fieldCalculationHelper = fieldCalculationHelper;
        }
        
        public override void Process(Entity entity)
        {
            var inputComponent = entity.GetComponent<PlayerInputComponent>();
            var laserComponent = entity.GetComponent<LaserWeaponComponent>();
            
            if (laserComponent.IsReady && inputComponent.LaserFire)
            {
                laserComponent.isActive = true;
                laserComponent.chargesLeft--;
                
                if (laserComponent.chargesLeft == laserComponent.ChargesCapacity - 1)
                    laserComponent.chargeCoolDownTimeLeft = laserComponent.ChargeCooldownDuration;
                
                laserComponent.LaserWeapon.Switch(true);
                laserComponent.activeTimeLeft = laserComponent.ActivityDuration;
            }

            if (laserComponent.isActive)
            {
                var transform = entity.GameObject.transform;
            
                if (_fieldCalculationHelper.GetForwardIntersectionPosition(out var endPosition,
                    transform.position, transform.up))
                {
                    var startPosition = transform.position;
                    laserComponent.LaserWeapon.SetLaser(startPosition, endPosition);
                }
            
                laserComponent.activeTimeLeft -= Time.deltaTime;
            
                if (laserComponent.activeTimeLeft <= 0)
                {
                    laserComponent.isActive = false;
                    laserComponent.LaserWeapon.Switch(false);
                } 
            }

            if (laserComponent.chargesLeft < laserComponent.ChargesCapacity)
            {
                if (laserComponent.chargeCoolDownTimeLeft <= 0)
                {
                    laserComponent.chargesLeft++;
                    laserComponent.chargeCoolDownTimeLeft = laserComponent.ChargeCooldownDuration;
                }
                else
                {
                    laserComponent.chargeCoolDownTimeLeft -= Time.deltaTime;
                }
            }
        }

        public override void PostProcess()
        {
            
        }

        public override void Destroy()
        {
            
        }
    }
}
