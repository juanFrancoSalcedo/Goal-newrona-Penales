using UnityEngine;
using UnityEngine.Events;

namespace Features.Score
{
    public class ScoreReceptor : MonoBehaviour, IScoreReceptor
    {
        [SerializeField] private int score;
        public UnityEvent OnScoreApplied;

        public int Score => score;

        public void ApplyScore(Vector3 hitPoint)
        {
            OnScoreApplied?.Invoke();
            print($"Score applied: {score}");
        }
    }
}