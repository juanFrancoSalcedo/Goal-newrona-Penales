using UnityEngine;

namespace Features.Score
{
    public class ScoreCardCreator : MonoBehaviour
    {
        [SerializeField] private ScoreCard prefab;
        [SerializeField] private Transform parent;

        private void Awake()
        {
            if (parent == null)
                parent = transform;
        }

        private void OnEnable() => ScoreMediator.Subscribe(ScoreEventType.ScoreApplied, OnScoreApplied);
        private void OnDisable() => ScoreMediator.Unsubscribe(ScoreEventType.ScoreApplied, OnScoreApplied);

        private void OnScoreApplied(int score)
        {
            if (prefab == null) return;
            var card = Instantiate(prefab, parent);
            card.Show(score);
        }
    }
}
