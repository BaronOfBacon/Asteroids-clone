using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ECS
{
    public class Entity
    {
        public IReadOnlyDictionary<Type, Component> Components => _components;

        private Action EntityChanged;
        private Dictionary<Type, Component> _components = new Dictionary<Type, Component>();

        public Entity(Action entityChangedCallback)
        {
            EntityChanged = entityChangedCallback;
        }

        public bool ValidForMask(IEnumerable<Type> componentTypes)
        {
            return componentTypes.All(type => _components.ContainsKey(type));
        }
        
        public void AddComponent(Component component)
        {
            if (!_components.TryAdd(component.GetType(), component))
            {
                Debug.LogError($"Can't add multiple component of type {component.GetType()}.");
                return;
            }
            
            _components.Add(component.GetType(), component);
            EntityChanged?.Invoke();
        }

        public void RemoveComponent(Type componentType)
        {
            if (!componentType.IsAssignableFrom(typeof(Component)))
            {
                Debug.LogError("Current type is not ECS.Component");
                return;
            }
            
            if (!_components.ContainsKey(componentType))
            {
                Debug.Log($"Entity hasn't contain {componentType}.");
                return;
            }

            _components.Remove(componentType);
            EntityChanged?.Invoke();
        }

        public void Destroy()
        {
            _components = null;
        }
    }
}
