using DG.Tweening;
using Services;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Features
{
    public class CountdownTimer : MonoBehaviour,IAdminListener
    {
        [SerializeField] private float _duration = 3f;
        [SerializeField] private TMP_Text _displayText;
        [SerializeField] private string _zeroPlaceholder = "Start";
        [SerializeField] private float _punchScale = 0.3f;
        [SerializeField] private float _punchDuration = 0.2f;
        [SerializeField] private UnityEvent OnFinish;
        public event Action OnCountdownFinished;

        private float _remaining;
        private bool _isRunning;
        private int _previousValue;

        private void OnEnable()
        {
            if (CountdownDataService.HasDuration())
                _duration = CountdownDataService.LoadDuration();
        }

        public void Begin()
        {
            _remaining = _duration;
            _isRunning = true;
            _previousValue = int.MaxValue;
            UpdateDisplay();
        }

        public void SetDuration(float duration) => _duration = duration;

        public void Stop() => _isRunning = false;

        private void Update()
        {
            if (!_isRunning)
                return;

            _remaining -= Time.deltaTime;
            UpdateDisplay();

            if (_remaining <= 0f)
            {
                _remaining = 0f;
                _isRunning = false;
                UpdateDisplay();
                OnFinish?.Invoke();
                OnCountdownFinished?.Invoke();
            }
        }

        private void UpdateDisplay()
        {
            if (_displayText == null)
                return;

            var currentValue = Mathf.CeilToInt(_remaining);
            _displayText.text = currentValue > 0 ? currentValue.ToString() : _zeroPlaceholder;

            if (currentValue != _previousValue)
            {
                _displayText.transform.DOPunchScale(Vector3.one * _punchScale, _punchDuration);
                _previousValue = currentValue;
            }
        }

        public void UpdateBehaviour()
        {
            _duration = CountdownDataService.LoadDuration();
        }
    }
}
