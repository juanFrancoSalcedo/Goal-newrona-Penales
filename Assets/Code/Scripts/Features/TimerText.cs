using TMPro;
using UnityEngine;

namespace Features
{
    [RequireComponent(typeof(TMP_Text))]
    public class TimerText : MonoBehaviour
    {
        [SerializeField] private Timer timer;
        [SerializeField] private TimerText timerText;

        private TMP_Text text;

        private void Awake()
        {
            text = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            if (timer != null)
                timer.OnUpdateTime += UpdateTime;
            if (timerText != null)
                text.text = timerText.GetTime();
        }

        private void OnDisable()
        {
            if (timer != null)
                timer.OnUpdateTime -= UpdateTime;
        }

        private void UpdateTime(string timeString) => text.text = timeString;

        public string GetTime() => text.text;
    }
}