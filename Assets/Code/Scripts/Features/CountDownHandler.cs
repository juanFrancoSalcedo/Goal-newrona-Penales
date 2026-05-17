using Features;
using Features.Score;
using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CountdownTimer))]
public class CountDownHandler : MonoBehaviour 
{
    [SerializeField] private GameEventType gameEventType;
    [SerializeField] private GameObject panelCoundownTimer;
    [SerializeField] private Timer gameTimer;
    CountdownTimer countdownTimer;

    private void Awake() => countdownTimer = GetComponent<CountdownTimer>();

    private void OnEnable()
    {
        GameStateContext.GameStateMediator.Subscribe(gameEventType, StartCountDown);
        ShotMediator.Subscribe(ShotEventType.ShotApplied, OnShotApplied);
        countdownTimer.OnCountdownFinished += FinishCountDown;
    }
    private void OnDisable()
    {
        GameStateContext.GameStateMediator.Unsubscribe(gameEventType, StartCountDown);
        ShotMediator.Unsubscribe(ShotEventType.ShotApplied, OnShotApplied);
        countdownTimer.OnCountdownFinished -= FinishCountDown;
    }

    private void FinishCountDown()
    {
        gameTimer.ResumeTimer();
    }

    private void OnShotApplied(TypeShot arg0)
    {
        StartCoroutine(DoShow());
    }
    private IEnumerator DoShow() 
    {
        yield return new WaitForSeconds(1.9f);
        if (gameTimer != null)
            gameTimer.PauseTimer();

        panelCoundownTimer.SetActive(true);
        StartCountDown();
    }


    private void StartCountDown() => countdownTimer.Begin();
}