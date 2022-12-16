using UnityEngine;

namespace Asteroids.LaserWeapon
{
    public class LaserWeaponView : MonoBehaviour
    {
        public LineRenderer LineRenderer => _lineRenderer;

        [SerializeField] 
        private LineRenderer _lineRenderer;
        [SerializeField] 
        private BoxCollider2D _collider;
        [SerializeField] 
        private float _beamWidth = 0.1f;
        
        public void SetLaser(Vector3 startPosition, Vector3 endPosition)
        {
            _lineRenderer.SetPosition(0, startPosition);
            _lineRenderer.SetPosition(1, endPosition);
            var beamLength = Vector3.Distance(endPosition, startPosition);
            var colliderVerticalOffset = beamLength / 2f;
            _collider.offset = new Vector2(0, colliderVerticalOffset);
            _collider.size = new Vector2(_beamWidth, beamLength);
        }

        public void Switch(bool state)
        {
            _lineRenderer.enabled = state;
            _collider.enabled = state;
        }
    }
}