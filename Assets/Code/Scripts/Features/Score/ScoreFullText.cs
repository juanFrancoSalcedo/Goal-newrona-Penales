using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Features.Score
{
    [RequireComponent(typeof(TMP_Text))]
    public class ScoreFullText : MonoBehaviour
    {
        [SerializeField] private string format = "Score: {0}";
        [SerializeField] private float punchScale = 0.3f;
        [SerializeField] private float punchDuration = 0.2f;

        private TMP_Text text;

        private void Awake() => text = GetComponent<TMP_Text>();
        private void OnEnable()
        {
            OnTotalScoreChanged(ScoreManager.Instance.TotalScore);
            ScoreMediator.Subscribe(ScoreEventType.TotalScoreChanged, OnTotalScoreChanged);
        }

        private void OnDisable() => ScoreMediator.Unsubscribe(ScoreEventType.TotalScoreChanged, OnTotalScoreChanged);


        private void OnTotalScoreChanged(int total)
        {
            text.text = string.Format(format, total);
            text.transform.DOPunchScale(Vector3.one * punchScale, punchDuration);
        }
    }
}
