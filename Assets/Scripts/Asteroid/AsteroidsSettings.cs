using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Asteroid
{
    [CreateAssetMenu(fileName = "AsteroidsSettings", menuName = "Data/AsteroidsSettings")]
    public class AsteroidsSettings : ScriptableObject
    {
        public GameObject AsteroidPrefab => _asteroidPrefab;
        public GameObject AsteroidFractionPrefab => asteroidFractionPrefab;
        public int AsteroidsSpawnAmount => _asteroidsSpawnAmount; 
        public int AsteroidFragmentsSpawnAmount => _asteroidFragmentsSpawnAmount; 
        public Vector2 AsteroidVelocityRange => _asteroidVelocityRange; 
        public Vector2 AsteroidFragmentVelocityRange => _asteroidFragmentVelocityRange;
        public Vector2 AsteroidFragmentRandomAngleRange => _asteroidFragmentRandomAngleRange;
        public float NewAsteroidSpawnCooldown => newAsteroidSpawnCooldown;
        
        [SerializeField]
        private GameObject _asteroidPrefab;
        [SerializeField]
        private GameObject asteroidFractionPrefab;
        [SerializeField]
        private int _asteroidsSpawnAmount;
        [SerializeField] 
        private int _asteroidFragmentsSpawnAmount;
        [SerializeField] 
        private Vector2 _asteroidVelocityRange;
        [SerializeField] 
        private Vector2 _asteroidFragmentVelocityRange;
        [SerializeField]
        private Vector2 _asteroidFragmentRandomAngleRange;
        [SerializeField] 
        private float newAsteroidSpawnCooldown;
    }
}
