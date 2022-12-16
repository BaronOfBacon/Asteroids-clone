using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ECS.Messages
{
    public class Dispatcher
    {
        private Dictionary<MessageType, Action<object>> subscriptions
            = new Dictionary<MessageType, Action<object>>();

        public bool Subscribe(MessageType type, Action<object> callback)
        {
            if (!HasSubscriptionWithMessageType(type))
            {
                subscriptions.Add(type, delegate(object o) { });
            }

            subscriptions[type] += callback;
            return true;
        }

        public bool Unsubscribe(MessageType type, Action<object> callback)
        {
            if (!HasSubscriptionWithMessageType(type)) return false;

            var subscribersList = subscriptions[type].GetInvocationList().ToList();
            if (callback == null)
            {
                Debug.LogError("Callback is null!");
            }

            if (!subscribersList.Exists(subscriber => subscriber.Method.Equals(callback.Method))) return false;

            subscriptions[type] -= callback;
            return true;
        }

        public bool SendMessage(MessageType type, object args)
        {
            if (!HasSubscriptionWithMessageType(type)) return false;

            subscriptions[type].DynamicInvoke(args);
            return true;
        }

        private bool HasSubscriptionWithMessageType(MessageType type)
        {
            return subscriptions.ContainsKey(type);
        }
    }
}