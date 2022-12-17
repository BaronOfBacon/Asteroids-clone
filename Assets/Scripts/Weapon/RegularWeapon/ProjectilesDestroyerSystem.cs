using System;
using System.Collections.Generic;
using Asteroids.Weapon.Projectile;
using ECS;
using ECS.Messages;

namespace Asteroids.Weapon.RegularWeapon
{
    public class ProjectilesDestroyerSystem : ECS.System
    {
        public override IEnumerable<Type> ComponentsMask => _componentsMask;
        private IEnumerable<Type> _componentsMask => new List<Type>()
        {
            typeof(ProjectileComponent)
        };

        private bool _destroy;
        
        public override void Process(Entity entity)
        {
            if (!_destroy) return;
            entity.InitDestroy();
        }

        public override void Initialize(World world, Dispatcher messageDispatcher)
        {
            base.Initialize(world, messageDispatcher);
            MessageDispatcher.Subscribe(MessageType.PlayerDied, DestroyAllProjectiles);
        }

        public override void PostProcess()
        {
            if (!_destroy) return;
            _destroy = false;
        }
        
        public void DestroyAllProjectiles(object arg)
        {
            _destroy = true;
        }

        public override void Destroy()
        {
            MessageDispatcher.Unsubscribe(MessageType.PlayerDied, DestroyAllProjectiles);
        }
    }
}
