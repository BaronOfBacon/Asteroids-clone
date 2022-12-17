using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Player
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "Data/PlayerSettings")]
    public class PlayerSettings : ScriptableObject
    {
        public GameObject PlayerPrefab => _playerPrefab;
        public float ForwardAccelerationMultiplier => _forwardAccelerationMultiplier;
        public float PlayerRotationSpeed => _playerRotationSpeed;
        public float PlayerFriction => _playerFriction;
        
        [SerializeField]
        private GameObject _playerPrefab;
        [SerializeField] 
        private float _forwardAccelerationMultiplier = 5f;
        [SerializeField] 
        private float _playerRotationSpeed = 0.3f;
        [SerializeField]
        private float _playerFriction = 1f;
    }
}
