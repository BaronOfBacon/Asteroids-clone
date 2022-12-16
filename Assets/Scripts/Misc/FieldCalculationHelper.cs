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

        public bool GetIntersections(out List<Vector3> intersections,  Vector2 position, Vector2 direction)
        {
            intersections = new List<Vector3>();
            
            foreach (var vertex in _vertices)
            {
                var vertexPosition = vertex.Position;
                var vertexDirection = vertex.neighbourVertices[0].Position - vertex.Position;
                if (MathHelper.LineLineIntersection(out var intersection, position, direction,
                    vertexPosition, vertexDirection))
                {
                    if (IsInsideOfBoundaries(intersection))
                    {
                        intersections.Add(intersection);
                    }
                }
            }
            
            return intersections.Any();
        }
        
        public bool GetClosestIntersectionPosition(out Vector2 newPosition, Vector2 position, Vector2 direction)
        {
            newPosition = new Vector2();
            
            if (!GetIntersections(out var intersections, position, direction))
            {
                return false;
            }
            
            newPosition = intersections[0];
            var minDistance = Vector3.Distance(newPosition, position);
            intersections.RemoveAt(0);
            while (intersections.Any())
            {
                var comparablePosition = intersections[0];
                
                var distance = Vector3.Distance(comparablePosition, position);
                
                if(distance < minDistance)
                {
                    newPosition = comparablePosition;
                    minDistance = distance;
                }
                intersections.RemoveAt(0);
            }

            return true;
        }
        
        public bool GetForwardIntersectionPosition(out Vector2 newPosition, Vector2 position, Vector2 direction)
        {
            newPosition = new Vector2();

            if (!GetIntersections(out var intersections, position, direction))
            {
                return false;
            }
            
            newPosition = intersections[0];
            intersections.RemoveAt(0);
            while (intersections.Any())
            {
                var comparablePosition = intersections[0];
                var dotProduct = Vector3.Dot(direction, comparablePosition - (Vector3)position);
                if (Mathf.Sign(dotProduct) == 1)
                {
                    newPosition = comparablePosition;
                }
                intersections.RemoveAt(0);
            }

            return true;
        }
        
        public bool NewPositionInPortal(out Vector2 newPosition, Vector2 position, Vector2 direction)
        {
            if (!GetClosestIntersectionPosition(out newPosition, position, direction))
            {
                return false;
            }

            if (Mathf.Abs(newPosition.x) >= _fieldBoundariesDistance.x)
                newPosition.x = -newPosition.x;
            if (Mathf.Abs(newPosition.y) >= _fieldBoundariesDistance.y)
                newPosition.y = -newPosition.y;
            
            return true;
        }

        public bool IsInsideOfBoundaries(Vector2 position)
        {
            var precisionDelta = 0.01f;
            return Mathf.Abs(position.x) <= _fieldBoundariesDistance.x + precisionDelta &&
                   Mathf.Abs(position.y) <= _fieldBoundariesDistance.y + precisionDelta;
        }

        public Vector2 GetRandomPointFromInside()
        {
            var x = Random.Range(-_fieldBoundariesDistance.x, _fieldBoundariesDistance.x);
            var y = Random.Range(-_fieldBoundariesDistance.y, _fieldBoundariesDistance.y);
            return new Vector2(x, y);
        }
        
        private struct RectVertex
        {
            public Vector2 Position;
            public RectVertex[] neighbourVertices;
        }
    }
}
