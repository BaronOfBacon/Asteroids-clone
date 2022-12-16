using UnityEngine;

namespace Asteroids.Score
{
    [CreateAssetMenu(fileName = "ScoreSettingsData", menuName = "Data/ScoreSettings")]
    public class ScoreSettings : ScriptableObject
    {
        public int AsteroidKillPoints => _asteroidKillPoints;
        public int AsteroidFractionKillPoints => _asteroidKillPoints;
        
        [SerializeField] 
        private int _asteroidKillPoints;
        [SerializeField] 
        private int _asteroidFragmentKillPoints;
    }
}
