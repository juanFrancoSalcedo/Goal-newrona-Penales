using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] float targetTime = 0;
    float timeRemaing = 0;
    [SerializeField] private bool reduce = false;
    [SerializeField] private bool isPaused = false; // ← Nuevo campo

    public bool IsPaused => isPaused; // ← Propiedad pública de solo lectura

    public event Action<string> OnUpdateTime;
    public event Action OnTimeCompleted;

    Coroutine coroutineTimer;

    [SerializeField] private UnityEvent onStartTimer;
    [SerializeField] private UnityEvent<bool> onPauseTimer;
    [SerializeField] private UnityEvent onStopTimer;
    [SerializeField] private UnityEvent onTimeCompleted;

    public void StopTimer()
    {
        if (coroutineTimer != null)
            StopCoroutine(coroutineTimer);

        coroutineTimer = null;
        isPaused = false;
        onStopTimer?.Invoke();
    }

    public void StartTimer()
    {
        if (reduce)
            timeRemaing = targetTime;
        else
            timeRemaing = 0;

        if (coroutineTimer == null)
            coroutineTimer = StartCoroutine(DoTimer());

        isPaused = false;
        onStartTimer?.Invoke();
    }

    public void PauseTimer()
    {
        if (!isPaused && coroutineTimer != null)
        {
            isPaused = true;
            onPauseTimer?.Invoke(true);
        }
    }

    public void ResumeTimer()
    {
        if (isPaused && coroutineTimer != null)
        {
            isPaused = false;
            onPauseTimer?.Invoke(false);
        }
    }

    public void TogglePause()
    {
        if (isPaused)
            ResumeTimer();
        else
            PauseTimer();
    }

    public void RestartTimer()
    {
        coroutineTimer = null;
        StartTimer();
    }

    private IEnumerator DoTimer()
    {
        float amount = (reduce) ? -1 : 1;

        while (!ReachTime())
        {
            if (isPaused)
            {
                yield return null;
                continue;
            }

            var secs = TimeSpan.FromSeconds(timeRemaing);
            timeRemaing += (amount * Time.deltaTime);
            OnUpdateTime?.Invoke(secs.ToString("mm\\:ss"));
            yield return null;
        }

        coroutineTimer = null;
        onTimeCompleted?.Invoke();
        OnTimeCompleted?.Invoke();
    }

    public float GetCurrentTime() => timeRemaing;

    public bool ReachTime()
    {
        if (reduce)
            return timeRemaing <= 0;
        else
            return timeRemaing >= targetTime;
    }
}

