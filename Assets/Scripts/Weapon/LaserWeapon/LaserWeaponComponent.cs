using Asteroids.LaserWeapon;
using Component = ECS.Component;

namespace Asteroids.Weapon.LaserWeapon
{
    public class LaserWeaponComponent : Component
    {
        public LaserWeaponView LaserWeapon { get; private set; }
        public float ActivityDuration { get; private set; }
        public int ChargesCapacity { get; private set; }
        public float ChargeCooldownDuration { get; private set; }
        public bool IsReady => chargesLeft > 0 && !isActive;

        public int chargesLeft;
        public float activeTimeLeft;
        public float chargeCoolDownTimeLeft;
        
        public bool isActive = false;
        
        public LaserWeaponComponent(LaserWeaponView laserWeapon, float activityDuration, int chargesCapacity, 
            float chargeCooldownDuration)
        {
            LaserWeapon = laserWeapon;
            ActivityDuration = activityDuration;
            ChargesCapacity = chargesCapacity;
            chargesLeft = chargesCapacity;
            ChargeCooldownDuration = chargeCooldownDuration;
            chargeCoolDownTimeLeft = chargeCooldownDuration;
        }
    }
}
