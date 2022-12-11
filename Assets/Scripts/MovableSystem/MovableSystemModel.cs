using System;
using System.Collections.Generic;
using System.Linq;
using Asteroids.Movable;
using Core;
using UnityEngine;

namespace Asteroids.MovableSystem
{
    public class MovableSystemModel : Model
    {
        public EventHandler MovablesChanged;
        public float forwardAccelerationMultiplier;
        public float maxSpeed;
        
        public IReadOnlyList<MovableFacade> Movables => _movables;

        private List<MovableFacade> _movables = new List<MovableFacade>();
        private List<MovableFacade> _movablesToDestroy = new List<MovableFacade>();

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

            if (_movablesToDestroy.Contains(movableToDelete))
            {
                Debug.LogWarning($"Already marked to delete!");
                return;
            }
            
            _movablesToDestroy.Add(movableToDelete);
            //_movables.Remove(movableToDelete);
            
            //Debug.Log($"Movable with name {movableToDelete.Transform} was deleted.");
        }

        public void DestroyMarkedMovables()
        {
            while (_movablesToDestroy.Any())
            {
                var movable = _movablesToDestroy[0];
                _movables.Remove(movable);
                _movablesToDestroy.Remove(movable);
                movable.Destroy();
            }
            MovablesChanged?.Invoke(this, null);
        }
    }
}
