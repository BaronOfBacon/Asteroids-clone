using Asteroids.MovableSystem;
using UnityEngine;

namespace Asteroids.GameManager
{
    public class GameInitiator : MonoBehaviour
    {
        [SerializeField]
        private GameManagerView _gameManagerView;
        [SerializeField]
        private MovableSystemView _movableSystemView;
        [SerializeField]
        private GameObject _movableViewPrefab;
        [SerializeField] 
        private GameSettings _gameSettings;
        
        private void Start()
        {
            GameManagerFactory factory = new GameManagerFactory();
            var gameManager = factory.Create(_gameManagerView);
            gameManager.StartGame(_movableSystemView, _movableViewPrefab, _gameSettings);
        }
    }
}
