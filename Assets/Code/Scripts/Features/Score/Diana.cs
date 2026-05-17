using UnityEngine;
using UnityEngine.Events;

namespace Features.Score
{
    public class Diana : MonoBehaviour, IScoreReceptor
    {
        [SerializeField] private ScoreRange[] scoreRanges;

        public UnityEvent<int> OnScoreApplied;

        private int _lastScore;

        public int Score => _lastScore;

        public void ApplyScore(Vector3 hitPoint, TypeShot typeShot)
        {
            float distanceToCenter = Vector2.Distance(
                new Vector2(hitPoint.x, hitPoint.y),
                new Vector2(transform.position.x, transform.position.y));

            _lastScore = 0;
            foreach (ScoreRange range in scoreRanges)
            {
                if (distanceToCenter >= range.minDistance && distanceToCenter <= range.maxDistance)
                {
                    _lastScore = range.score;
                    break;
                }
            }

            OnScoreApplied?.Invoke(_lastScore);
            ScoreMediator.Publish(ScoreEventType.ScoreApplied, _lastScore);
            ShotMediator.Publish(ShotEventType.ShotApplied, typeShot);
            print($"Diana hit! Distance: {distanceToCenter:F2} | Score: {_lastScore} | TypeShot: {typeShot}");
        }
    }
}