using TMPro;
using UnityEngine;

namespace Asteroids.UI.Game
{
    public class GameUIPanelView : MonoBehaviour
    {
        private const string COORDINATES_PREFIX = "Coordinates: ";
        private const string ANGLE_PREFIX = "Angle: ";
        private const string SPEED_PREFIX = "Speed: ";
        private const string LASER_CHARGES_PREFIX = "Laser charges: ";
        private const string LASER_CHARGES_COOLDOWN_PREFIX = "Laser charge cooldown: ";
        
        [SerializeField] 
        private TextMeshProUGUI _coordinatesLabel;
        [SerializeField] 
        private TextMeshProUGUI _angleLabel;
        [SerializeField] 
        private TextMeshProUGUI _speedLabel;
        [SerializeField] 
        private TextMeshProUGUI _laserChargesLabel;
        [SerializeField] 
        private TextMeshProUGUI _laserChargeCoolDown;

        public void UpdateCoordinates(Vector2 position)
        {
            _coordinatesLabel.SetText(COORDINATES_PREFIX + $" [{position.x:#.##}:{position.y:#.##}]");
        }
        
        public void UpdateAngle(float angle)
        {
            _angleLabel.SetText(ANGLE_PREFIX + $" [{angle:#}]");
        }
        
        public void UpdateSpeed(Vector2 velocity)
        {
            _speedLabel.SetText(SPEED_PREFIX + $" [{velocity.x:#.##}:{velocity.y:#.##}]");
        }
        
        public void UpdateLaserCharges(int charges)
        {
            _laserChargesLabel.SetText(LASER_CHARGES_PREFIX + $" [{charges:#}]");
        }

        public void UpdateLaserChargesCooldown(float cooldown)
        {
            _laserChargeCoolDown.SetText(LASER_CHARGES_COOLDOWN_PREFIX + $" [{cooldown:#.##}]");
        }
    }
}

