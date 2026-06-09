using Features;
using System;
using UnityEngine;

public class TimerGameEventHandler:MonoBehaviour
{
    [SerializeField] Timer timer;
    EndGameManager endGameManager;

    private void Start()
    {
        GameStateContext.GameStateMediator.Subscribe(GameEventType.GameStarted, OnGameStarted);
        GameStateContext.GameStateMediator.Subscribe(GameEventType.GameFinished, StopTimer);
        timer.OnTimeCompleted += OnTimeCompleted;
        endGameManager = EndGameManager.Instance;
    }

    private void StopTimer()
    {
        timer.StopTimer();
    }

    private void OnGameStarted()
    {
        timer.StartTimer();
        Debug.Log("Game Started");
    }

    private void OnTimeCompleted()
    {
        endGameManager.SetfullAttempts();
    }

    private void OnDestroy()
    {
        GameStateContext.GameStateMediator.Unsubscribe(GameEventType.GameStarted, OnGameStarted);
        GameStateContext.GameStateMediator.Unsubscribe(GameEventType.GameFinished, StopTimer);
        timer.OnTimeCompleted -= OnTimeCompleted;
    }
}