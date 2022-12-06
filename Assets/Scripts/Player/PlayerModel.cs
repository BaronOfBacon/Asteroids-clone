using Asteroids.Movable;
using Core;

namespace Asteroids.Player
{
    public class PlayerModel : Model
    {
        public float RotationForce => InputObserver.RotationInputValue * _rotationSpeed;
        public MovableFacade Movable { get; private set; }
        public IPlayerInputObserver InputObserver { get; private set; }

        private float _rotationSpeed;
        
        public PlayerModel(MovableFacade movable, IPlayerInputObserver inputObserver, float rotationSpeed)
        {
            Movable = movable;
            InputObserver = inputObserver;
            _rotationSpeed = rotationSpeed;
        }

        public void Update()
        {
            
        }
    }
}
