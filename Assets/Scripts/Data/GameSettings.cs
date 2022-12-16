using UnityEngine;

namespace Data
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
        public float LaserChargeCoolDown => _laserChargeCoolDown;
        public GameObject AsteroidPrefab => _asteroidPrefab;
        public GameObject AsteroidFragmentPrefab => _asteroidFragmentPrefab;
        public int AsteroidsSpawnAmount => _asteroidsSpawnAmount; 
        public int AsteroidFragmentsSpawnAmount => _asteroidFragmentsSpawnAmount; 
        public Vector2 AsteroidVelocityRange => _asteroidVelocityRange; 
        public Vector2 AsteroidFragmentVelocityRange => _asteroidFragmentVelocityRange;
        public LayerMask AsteroidDamageLayerMask => _asteroidDamageLayerMask;
        public LayerMask AsteroidDestroyLayerMask => _asteroidDestroyLayerMask;
        public LayerMask AsteroidIgnoreLayerMask => _asteroidIgnoreLayerMask;
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
        private float _laserChargeCoolDown = 2f;
        #endregion
        
        #region Asteroids
        [Header("Asteroids")] 
        [Space] 
        [SerializeField]
        private GameObject _asteroidPrefab;
        [SerializeField]
        private GameObject _asteroidFragmentPrefab;
        [SerializeField]
        private int _asteroidsSpawnAmount;
        [SerializeField] 
        private int _asteroidFragmentsSpawnAmount;
        [SerializeField] 
        private Vector2 _asteroidVelocityRange;
        [SerializeField] 
        private Vector2 _asteroidFragmentVelocityRange;
        [SerializeField] 
        private LayerMask _asteroidDamageLayerMask;
        [SerializeField] 
        private LayerMask _asteroidDestroyLayerMask;
        [SerializeField] 
        private LayerMask _asteroidIgnoreLayerMask;
        #endregion


    }
}