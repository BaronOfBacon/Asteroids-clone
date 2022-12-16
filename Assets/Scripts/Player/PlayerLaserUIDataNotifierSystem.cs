using System;
using System.Collections.Generic;
using Asteroids.Weapon.LaserWeapon;
using ECS;
using ECS.Messages;

namespace Asteroids.Player
{
    public class PlayerLaserUIDataNotifierSystem : ECS.System
    {
        public override IEnumerable<Type> ComponentsMask => _componentsMask;

        private IEnumerable<Type> _componentsMask;

        public PlayerLaserUIDataNotifierSystem()
        {
            _componentsMask = new List<Type>()
            {
                typeof(LaserWeaponComponent)
            };
        }
            
        public override void Process(Entity entity)
        {
            var laserComponent = entity.GetComponent<LaserWeaponComponent>();
            var laserData = new Tuple<int, float>(laserComponent.chargesLeft, laserComponent.chargeCoolDownTimeLeft);
            MessageDispatcher.SendMessage(MessageType.LaserDataChanged, laserData);
        }

        public override void PostProcess()
        {
            
        }

        public override void Destroy()
        {
            
        }
    }
}
