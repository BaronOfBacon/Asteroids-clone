using System.Collections.Generic;
using System.Linq;
using Tools;
using UnityEngine;

namespace Asteroids.Helpers
{
    public class FieldCalculationHelper
    {
        private Vector2 _fieldBoundariesDistance;
        private List<RectVertex> _vertices;
        
        public FieldCalculationHelper(Vector2 fieldBoundariesDistance)
        {
            _fieldBoundariesDistance = fieldBoundariesDistance;

            _vertices = new List<RectVertex>
            {
                new RectVertex(){Position = new Vector2(-_fieldBoundariesDistance.x,_fieldBoundariesDistance.y), neighbourVertices = new RectVertex[2]},
                new RectVertex(){Position = new Vector2(_fieldBoundariesDistance.x,_fieldBoundariesDistance.y), neighbourVertices = new RectVertex[2]},
                new RectVertex(){Position = new Vector2(+_fieldBoundariesDistance.x,-_fieldBoundariesDistance.y), neighbourVertices = new RectVertex[2]},
                new RectVertex(){Position = new Vector2(-_fieldBoundariesDistance.x,-_fieldBoundariesDistance.y), neighbourVertices = new RectVertex[2]}
            };

            for (var i = 0; i < _vertices.Count; i++)
            {
                var leftNeighbourIndex = i != _vertices.Count - 1 ?  i+1 : 0 ;
                _vertices[i].neighbourVertices[0] = _vertices[leftNeighbourIndex];
            }
            for (var i = 0; i < _vertices.Count; i++)
            {
                var rightNeighbourIndex = i != 0 ?  i-1 : _vertices.Count - 1 ;
                _vertices[i].neighbourVertices[0] = _vertices[rightNeighbourIndex];
            }
        }

        public bool GetForwardIntersectionPosition(out Vector2 newPosition, Vector2 position, Vector2 direction)
        {
            newPosition = new Vector2();
            List<Vector3> _intersections = new List<Vector3>();
            
            foreach (var vertex in _vertices)
            {
                var vertexPosition = vertex.Position;
                var vertexDirection = vertex.neighbourVertices[0].Position - vertex.Position;
                if (MathHelper.LineLineIntersection(out var intersection, position, direction,
                    vertexPosition, vertexDirection))
                {
                    if (IsInsideOfBoundaries(intersection))
                    {
                        _intersections.Add(intersection);
                    }
                }
            }
            
            if (!_intersections.Any())
            {
                return false;
            }
            
            newPosition = _intersections[0];
            _intersections.RemoveAt(0);
            while (_intersections.Any())
            {
                var comparablePosition = _intersections[0];
                var dotProduct = Vector3.Dot(direction, comparablePosition - (Vector3)position);
                if (Mathf.Sign(dotProduct) == 1)
                {
                    newPosition = comparablePosition;
                }
                _intersections.RemoveAt(0);
            }

            return true;
        }
        
        public bool NewPositionInPortal(out Vector2 newPosition, Vector2 position, Vector2 direction)
        {
            if (!GetForwardIntersectionPosition(out newPosition, position, direction))
            {
                return false;
            }
            
            if (Mathf.Abs(newPosition.x) >= _fieldBoundariesDistance.x)
                newPosition.x = -newPosition.x - Mathf.Sign(newPosition.x) * Time.deltaTime;
            if (Mathf.Abs(newPosition.y) >= _fieldBoundariesDistance.y)
                newPosition.y = -newPosition.y - Mathf.Sign(newPosition.y) * Time.deltaTime;

            return true;
        }

        public bool IsInsideOfBoundaries(Vector2 position)
        {
            var precisionDelta = 0.01f;
            return Mathf.Abs(position.x) <= _fieldBoundariesDistance.x + precisionDelta &&
                   Mathf.Abs(position.y) <= _fieldBoundariesDistance.y + precisionDelta;
        }

        private struct RectVertex
        {
            public Vector2 Position;
            public RectVertex[] neighbourVertices;
        }
    }
}
