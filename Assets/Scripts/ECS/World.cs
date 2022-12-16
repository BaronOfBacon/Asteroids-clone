using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ECS
{
    public class World
    {
        private List<Entity> _entities;
        private List<Entity> _entitiesToDestroy;
        private Dictionary<Type, System> _systemsDictionary;
        private Dictionary<System, List<Entity>> _sortedEntitiesAndSystems;

        public World()
        {
            _entities = new List<Entity>();
            _entitiesToDestroy = new List<Entity>();
            _systemsDictionary = new Dictionary<Type, System>();
            _sortedEntitiesAndSystems = new Dictionary<System, List<Entity>>();
        }
        
        public Entity CreateEntity(GameObject gameObject)
        {
            var entity = new Entity(RecalculateDependencies, gameObject, DestroyEntity);
            _entities.Add(entity);
            RecalculateDependencies();
            return entity;
        }
        
        public void DestroyEntity(Entity entity)
        {
            if (_entities.Contains(entity) && !_entitiesToDestroy.Contains(entity))
                _entitiesToDestroy.Add(entity);
        }

        public void AddSystem(System system)
        {
            var systemType = system.GetType();
            
            if (!_systemsDictionary.TryAdd(system.GetType(), system))
            {
                Debug.LogError($"Can't add multiple systems of type {systemType}.");
                return;
            }
            _sortedEntitiesAndSystems.Add(system, new List<Entity>());
        }

        public void RemoveSystem(Type systemType)
        {
            if (!systemType.IsAssignableFrom(typeof(System)))
            {
                Debug.LogError("Current type is not ECS.Component");
                return;
            }
            
            if (!_systemsDictionary.ContainsKey(systemType))
            {
                Debug.Log($"No registered systems with type: {systemType}.");
                return;
            }

            _systemsDictionary.Remove(systemType);
            
            var systemOfType = _sortedEntitiesAndSystems.First(pair => pair.Key.GetType() == systemType).Key;
            _sortedEntitiesAndSystems.Remove(systemOfType);
        }
        
        private void RecalculateDependencies()
        {
            foreach (var pair in _sortedEntitiesAndSystems)
            {
                #region Remove invalid entities
                var componentsMask = pair.Key.ComponentsMask;
                var invalidEntitiesStack = new Stack<Entity>();
                
                foreach (var entity in pair.Value)
                {
                    if (!entity.ValidForMask(componentsMask))
                    {
                        invalidEntitiesStack.Push(entity);
                    }
                }

                while (invalidEntitiesStack.TryPop(out var entity))
                {
                    pair.Value.Remove(entity);
                }
                #endregion

                #region Add valid
                foreach (var entity in _entities)
                {
                    if (pair.Value.Contains(entity)) continue;

                    if (entity.ValidForMask(componentsMask))
                    {
                        pair.Value.Add(entity);
                    }
                }
                #endregion
            }
        }

        public void Update()
        {
            foreach (var pair in _sortedEntitiesAndSystems)
            {
                foreach (var entity in pair.Value)
                {
                    pair.Key.Process(entity);   
                }
                pair.Key.PostProcess();
            }

            if (!_entitiesToDestroy.Any()) return;
            
            while (_entitiesToDestroy.Any())
            {
                var entity = _entitiesToDestroy[0];
                _entitiesToDestroy.RemoveAt(0);
                _entities.Remove(entity);
                
                entity.Destroy();
            }
            
            RecalculateDependencies();
        }

        public void Destroy()
        {
            foreach (var pair in _systemsDictionary)
            {
                pair.Value.Destroy();
            }
            
            _systemsDictionary.Clear();
            _sortedEntitiesAndSystems.Clear();
            
            foreach (var entity in _entities)
            {
                entity.InitDestroy();
            }
        }
    }
}
