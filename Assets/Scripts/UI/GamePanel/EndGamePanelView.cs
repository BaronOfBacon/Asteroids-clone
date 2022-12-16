using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.UI.Game
{
    public class EndGamePanelView : MonoBehaviour
    {
        public const string SCORE_PREFIX = "Score: ";
        
        public Button RestartButton => _restartButton;
        
        [SerializeField] 
        private TextMeshProUGUI _scoreLabel;
        [SerializeField] 
        private Button _restartButton;

        public void SetScore(int score)
        {
            _scoreLabel.SetText(SCORE_PREFIX + score);
        }

        public void Switch(bool state)
        {
            gameObject.SetActive(state);
        }
    }
}
