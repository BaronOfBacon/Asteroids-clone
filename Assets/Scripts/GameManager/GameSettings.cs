using UnityEngine;

namespace Asteroids.GameManager
{
    [CreateAssetMenu(menuName = "Data/GameSettings", fileName = "NewGameSettings")]
    public class GameSettings : ScriptableObject
    {
        #region Public fields
        public Vector2 GameFieldSize => _gameFieldSize;
        public GameObject PlayerPrefab => _playerPrefab;
        public float MaxSpeed => _maxSpeed;
        public float ForwardAccelerationMultiplier => _forwardAccelerationMultiplier;
        public float PlayerRotationSpeed => _playerRotationSpeed;
        public float PlayerFriction => _playerFriction;
        public GameObject ProjectilePrefab => _projectilePrefab;
        public float ProjectileSpeed => _projectileSpeed;
        public Vector2 ProjectileSpawnOffset => _projectileSpawnOffset;
        public int LaserChargesCapacity => _laserChargesCapacity;
        public float LaserActiveTimeDuration => _laserActiveTimeDuration;
        public float LaserChargeCooldown => laserChargeCooldown;
        
        #endregion

        #region General
        [Header("General")]
        [Space]
        [SerializeField] 
        private Vector2 _gameFieldSize = new (21,13);
        [SerializeField] 
        private float _maxSpeed = 100f;
        #endregion
        
        #region Player
        [Header("Player")] 
        [Space] 
        [SerializeField]
        private GameObject _playerPrefab;
        [SerializeField] 
        private float _forwardAccelerationMultiplier = 5f;
        [SerializeField] 
        private float _playerRotationSpeed = 0.3f;
        [SerializeField]
        private float _playerFriction = 1f;
        #endregion
        
        #region Regular weapon
        [Header("Regular weapon")]
        [Space]
        [SerializeField]
        private GameObject _projectilePrefab;
        [SerializeField] 
        private float _projectileSpeed = 1f;
        [SerializeField] 
        private Vector2 _projectileSpawnOffset = new Vector2(0f, 0.5f);
        #endregion

        #region Laser
        [Header("Laser")]
        [Space]
        [SerializeField] 
        private int _laserChargesCapacity = 4;
        [SerializeField] 
        private float _laserActiveTimeDuration = 0.5f;
        [SerializeField] 
        private float laserChargeCooldown = 2f;
        #endregion
        
    }
}
