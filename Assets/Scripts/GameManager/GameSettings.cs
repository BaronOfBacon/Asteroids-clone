using UnityEngine;

namespace Asteroids.GameManager
{
    [CreateAssetMenu(menuName = "Data/GameSettings", fileName = "NewGameSettings")]
    public class GameSettings : ScriptableObject
    {
        #region Public fields
        public Vector2 GameFieldSize => _gameFieldSize;
        public float MaxSpeed => _maxSpeed;
        public float ForwardAccelerationMultiplier => forwardAccelerationMultiplier;
        public float PlayerRotationSpeed => _playerRotationSpeed;
        public float PlayerFriction => _playerFriction;
        public GameObject ProjectilePrefab => _projectilePrefab;
        public float ProjectileSpeed => _projectileSpeed; 
        public int LaserChargesCapacity => laserChargesCapacity;
        public float LaserActiveTimeDuration => laserActiveTimeDuration;
        public float LaserChargeCoolDown => laserChargeCoolDown;
        #endregion

        [SerializeField] 
        private Vector2 _gameFieldSize = new (21,13);
        [SerializeField] 
        private float _maxSpeed = 100f;

        #region Player
        [SerializeField] 
        private float forwardAccelerationMultiplier = 5f;
        [SerializeField] 
        private float _playerRotationSpeed = 0.3f;
        [SerializeField]
        private float _playerFriction = 1f;
        #endregion
        
        #region Regular weapon
        [SerializeField]
        private GameObject _projectilePrefab;
        [SerializeField] 
        private float _projectileSpeed = 1f;
        #endregion

        #region Laser
        [SerializeField] 
        private int laserChargesCapacity = 4;
        [SerializeField] 
        private float laserActiveTimeDuration = 0.5f;
        [SerializeField] 
        private float laserChargeCoolDown = 2f;
        #endregion
    }
}
