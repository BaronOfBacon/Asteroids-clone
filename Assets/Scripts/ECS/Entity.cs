using System;
using System.Collections.Generic;
using UnityEngine;

namespace ECS
{
    public class Entity
    {
        public GameObject GameObject { get; private set; }
        public IReadOnlyDictionary<Type, Component> Components => _components;

        private Action EntityChanged;
        private Dictionary<Type, Component> _components = new Dictionary<Type, Component>();
        private Action<Entity> _destroyAction;
        
        public Entity(Action entityChangedCallback, GameObject gameObject, Action<Entity> destroyAction)
        {
            EntityChanged = entityChangedCallback;
            GameObject = gameObject;
            _destroyAction = destroyAction;
        }

        public bool ValidForMask(IEnumerable<Type> componentTypes)
        {
            foreach (var comparableType in componentTypes)
            {
                if (!_components.ContainsKey(comparableType))
                    return false;
            }

            return true;
        }
        
        public Component AddComponent(Component component)
        {
            if (!_components.TryAdd(component.GetType(), component))
            {
                Debug.LogError($"Can't add multiple component of type {component.GetType()}.");
                return null;
            }
            
            EntityChanged?.Invoke();
            return component;
        }

        public T AddComponent<T>() where T: Component
        {
            return (T)AddComponent(Activator.CreateInstance<T>());
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

        public T GetComponent<T>() where T: Component
        {
            _components.TryGetValue(typeof(T), out Component component);
            return (T)component;
        }

        public void InitDestroy()
        {
            _destroyAction?.Invoke(this);
        }

        public void Destroy()
        {
            _components.Clear();
            EntityChanged?.Invoke();
            GameObject.Destroy(GameObject);
        }
    }
}
