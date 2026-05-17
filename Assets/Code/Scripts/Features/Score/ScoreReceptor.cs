using UnityEngine;
using UnityEngine.Events;

namespace Features.Score
{
    public class ScoreReceptor : MonoBehaviour, IScoreReceptor
    {
        [SerializeField] private int score;
        public UnityEvent OnScoreApplied;

        public int Score => score;

        public void ApplyScore(Vector3 hitPoint, TypeShot typeShot)
        {
            if(GameStateContext.State == GameEventType.GameStarted)
            {
                OnScoreApplied?.Invoke();
                ScoreMediator.Publish(ScoreEventType.ScoreApplied, score);
                ShotMediator.Publish(ShotEventType.ShotApplied, score <= 0?TypeShot.Wrong:TypeShot.Goal);
                print($"Score applied: {score} | TypeShot: {typeShot}");
            }
        }
    }
}