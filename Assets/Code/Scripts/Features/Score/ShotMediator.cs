using System.Collections.Generic;
using UnityEngine.Events;

namespace Features.Score
{
    public enum ShotEventType
    {
        ShotApplied
    }

    public static class ShotMediator
    {
        private static readonly Dictionary<ShotEventType, UnityEvent<TypeShot>> events = new();

        public static void Subscribe(ShotEventType type, UnityAction<TypeShot> action)
        {
            if (events.TryGetValue(type, out var unityEvent))
            {
                unityEvent.AddListener(action);
            }
            else
            {
                var newEvent = new UnityEvent<TypeShot>();
                newEvent.AddListener(action);
                events.Add(type, newEvent);
            }
        }

        public static void Unsubscribe(ShotEventType type, UnityAction<TypeShot> action)
        {
            if (events.TryGetValue(type, out var unityEvent))
            {
                unityEvent.RemoveListener(action);
            }
        }

        public static void Publish(ShotEventType type, TypeShot typeShot)
        {
            if (events.TryGetValue(type, out var unityEvent))
            {
                unityEvent.Invoke(typeShot);
            }
        }
    }
}