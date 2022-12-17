using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.UFO
{
    [CreateAssetMenu(fileName = "PursuitPlayerSettings", menuName = "Data/PursuitPlayerSettings")]
    public class PursuitPlayerSettings : ScriptableObject
    {
        public float SpawnCooldown => _spawnCooldown;
        public GameObject PursuerPrefab => _persuerPrefab;
        public float PursuerSpeed => _persuerSpeed;
        
        [SerializeField] 
        private float _spawnCooldown;
        [SerializeField] 
        private GameObject _persuerPrefab;
        [SerializeField] 
        private float _persuerSpeed;
    }
}
