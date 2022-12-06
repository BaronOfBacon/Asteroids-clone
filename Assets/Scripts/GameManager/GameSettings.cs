using UnityEngine;

namespace Asteroids.GameManager
{
    [CreateAssetMenu(menuName = "Data/GameSettings", fileName = "NewGameSettings")]
    public class GameSettings : ScriptableObject
    {
        public Vector2 GameFieldSize => _gameFieldSize;
        public float MaxSpeed => _maxSpeed;
        public float ForwardAccelerationMultiplier => forwardAccelerationMultiplier;
        public float PlayerRotationSpeed => _playerRotationSpeed;
        
        [SerializeField] 
        private Vector2 _gameFieldSize = new (21,13);
        [SerializeField] 
        private float _maxSpeed = 100f;
        [SerializeField] 
        private float forwardAccelerationMultiplier = 5f;
        [SerializeField] 
        private float _playerRotationSpeed = 0.3f;

    }
}
