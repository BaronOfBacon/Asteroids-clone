using System;

namespace Asteroids.Player
{
    public interface IPlayerInputObserver
    {
        float RotationInputValue { get; }
        EventHandler FireInputDetected { get; set; }
        EventHandler LaserFireInputDetected { get; set; }
        EventHandler<bool> ThrustInputDetected { get; set; }
    }
}
