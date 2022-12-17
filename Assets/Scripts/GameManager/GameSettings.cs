using UnityEngine;

namespace Asteroids.GameManager
{
    [CreateAssetMenu(menuName = "Data/GameSettings", fileName = "NewGameSettings")]
    public class GameSettings : ScriptableObject
    {
        #region Public fields
        public Vector2 GameFieldSize => _gameFieldSize;
        
        public float MaxSpeed => _maxSpeed;
        
        
        
        #endregion

        #region General
        [Header("General")]
        [Space]
        [SerializeField] 
        private Vector2 _gameFieldSize = new (21,13);
        [SerializeField] 
        private float _maxSpeed = 100f;
        #endregion
        
        
    }
}
