using System;
using System.Collections.Generic;
using Asteroids.Movable;
using Core;
using UnityEngine;

namespace Asteroids.MovableSystem
{
    public class MovableSystemModel : Model
    {
        public EventHandler MovablesChanged;
        public Vector2 fieldBoundariesDistance;
        public float forwardAccelerationMultiplier;
        public float maxSpeed;
        
        public IReadOnlyList<MovableFacade> Movables => _movables;

        private List<MovableFacade> _movables = new List<MovableFacade>();

        public void AddMovable(MovableFacade newMovable)
        {
            if (_movables.Contains(newMovable))
            {
                Debug.LogWarning($"This movable is already added!");
                return;
            }
            
            _movables.Add(newMovable);
            MovablesChanged?.Invoke(this, null);
            Debug.Log($"Movable with name {newMovable.Transform} was added.");
        }

        public void DeleteMovable(MovableFacade movableToDelete)
        {
            if (!_movables.Contains(movableToDelete))
            {
                Debug.LogWarning($"There is no such movable in list!");
                return;
            }

            _movables.Remove(movableToDelete);
            MovablesChanged?.Invoke(this, null);
            Debug.Log($"Movable with name {movableToDelete.Transform} was deleted.");
        }
    }
}
