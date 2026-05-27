using Services;
using TMPro;
using UnityEngine;

namespace Features
{
    public class CountdownTimerSetter : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private CountdownTimer targetTimer;

        private void OnEnable()
        {
            if (CountdownDataService.HasDuration())
                inputField.text = CountdownDataService.LoadDuration().ToString("F0");

            inputField.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDisable() => inputField.onValueChanged.RemoveListener(OnValueChanged);

        private void OnValueChanged(string value)
        {
            if (float.TryParse(value, out float duration) && duration > 0)
            {
                CountdownDataService.SaveDuration(duration);
                targetTimer.SetDuration(duration);
                AdminManager.Instance.NotifyAll();
            }
        }
    }
}