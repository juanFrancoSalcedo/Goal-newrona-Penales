using System.Collections.Generic;
using UnityEngine.Events;

namespace Features.Score
{
    public enum ScoreEventType
    {
        ScoreApplied,
        TotalScoreChanged
    }

    public static class ScoreMediator
    {
        private static readonly Dictionary<ScoreEventType, UnityEvent<int>> events = new();

        public static void Subscribe(ScoreEventType type, UnityAction<int> action)
        {
            if (events.TryGetValue(type, out var unityEvent))
            {
                unityEvent.AddListener(action);
            }
            else
            {
                var newEvent = new UnityEvent<int>();
                newEvent.AddListener(action);
                events.Add(type, newEvent);
            }
        }

        public static void Unsubscribe(ScoreEventType type, UnityAction<int> action)
        {
            if (events.TryGetValue(type, out var unityEvent))
            {
                unityEvent.RemoveListener(action);
            }
        }

        public static void Publish(ScoreEventType type, int score)
        {
            if (events.TryGetValue(type, out var unityEvent))
            {
                unityEvent.Invoke(score);
            }
        }
    }
}
