using System;
using System.Collections.Generic;

namespace LOK1game.Game.Events
{
    public static class EventManager
    {
        private static readonly Dictionary<Type, Action<Event>> s_Events = new Dictionary<Type, Action<Event>>();

        private static readonly Dictionary<Delegate, Action<Event>> s_EventLookups =
            new Dictionary<Delegate, Action<Event>>();

        public static void AddListener<T>(Action<T> evt) where T : Event
        {
            if (!s_EventLookups.ContainsKey(evt))
            {
                Action<Event> newAction = (e) => evt((T)e);
                s_EventLookups[evt] = newAction;

                if (s_Events.TryGetValue(typeof(T), out Action<Event> internalAction))
                    s_Events[typeof(T)] = internalAction += newAction;
                else
                    s_Events[typeof(T)] = newAction;
            }
        }

        public static void RemoveListener<T>(Action<T> evt) where T : Event
        {
            if (!s_EventLookups.TryGetValue(evt, out var action)) return;
            
            if (s_Events.TryGetValue(typeof(T), out var tempAction))
            {
                tempAction -= action;
                if (tempAction == null)
                    s_Events.Remove(typeof(T));
                else
                    s_Events[typeof(T)] = tempAction;
            }

            s_EventLookups.Remove(evt);
        }

        public static void Broadcast(Event evt)
        {
            if (s_Events.TryGetValue(evt.GetType(), out var action))
                action.Invoke(evt);
        }

        public static void Clear()
        {
            s_Events.Clear();
            s_EventLookups.Clear();
        }
    }
}