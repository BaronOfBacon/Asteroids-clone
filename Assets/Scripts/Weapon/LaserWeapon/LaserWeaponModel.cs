using Asteroids.Helpers;
using Core;

namespace Asteroids.LaserWeapon
{
    public class LaserWeaponModel : Model
    {
        public FieldCalculationHelper FieldCalculationHelper { get; private set; }
        public float ActiveTimeDuration { get; private set; }
        public int ChargesMaxCapacity { get; private set; }
        public float ChargeCoolDownDuration { get; private set; }
        public int ChargesLeft => chargesLeft;
        public bool IsReady => chargesLeft > 0 && !isBusy;

        public int chargesLeft;
        public float activeTimeLeft;
        public float chargeCoolDownTimeLeft;
        
        public bool isBusy = false;
        
        public LaserWeaponModel(FieldCalculationHelper fieldCalculationHelper, float activeTimeDuration,
            int chargesCapacity, float chargeCoolDownDuration)
        {
            FieldCalculationHelper = fieldCalculationHelper;
            ActiveTimeDuration = activeTimeDuration;
            ChargesMaxCapacity = chargesCapacity;
            ChargeCoolDownDuration = chargeCoolDownDuration;
            chargeCoolDownTimeLeft = chargeCoolDownDuration;
            chargesLeft = ChargesMaxCapacity;
        }
    }
}
