using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Features.Score
{
    public class ScoreCard : MonoBehaviour
    {
        [SerializeField] private float displayDuration = 1.5f;
        [SerializeField] private float fadeDuration = 0.5f;
        [SerializeField] private float punchScale = 0.3f;
        [SerializeField] private float punchDuration = 0.2f;
        [SerializeField] private TMP_Text text;
        private Coroutine fadeRoutine;


        public void Show(int score)
        {
            text.text = $"+{score}";
            var c = text.color;
            c.a = 1f;
            text.color = c;

            text.transform.DOPunchScale(Vector3.one * punchScale, punchDuration);
            fadeRoutine = StartCoroutine(FadeOutRoutine());
        }

        private IEnumerator FadeOutRoutine()
        {
            yield return new WaitForSeconds(displayDuration);
            text.DOFade(0f, fadeDuration).OnComplete(() => Destroy(gameObject));
            fadeRoutine = null;
        }

        private void OnDestroy()
        {
            text.DOKill();
            if (fadeRoutine != null)
                StopCoroutine(fadeRoutine);
        }
    }
}
