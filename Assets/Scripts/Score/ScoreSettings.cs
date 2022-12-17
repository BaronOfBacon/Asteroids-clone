using UnityEngine;

namespace Asteroids.Score
{
    [CreateAssetMenu(fileName = "ScoreSettingsData", menuName = "Data/ScoreSettings")]
    public class ScoreSettings : ScriptableObject
    {
        public int AsteroidKillPoints => _asteroidKillPoints;
        public int AsteroidFractionKillPoints => _asteroidFractionKillPoints;
        public int PlayerPursuerKillPoints => _playerPursuerKillPoints;
        
        [SerializeField] 
        private int _asteroidKillPoints;
        [SerializeField] 
        private int _asteroidFractionKillPoints;
        [SerializeField] 
        private int _playerPursuerKillPoints;
    }
}
