using System;
using UnityEngine;

public class TimerGameEventHandler:MonoBehaviour
{
    [SerializeField] Timer timer;
    private void Start()
    {
        GameStateContext.GameStateMediator.Subscribe(GameEventType.GameStarted, OnGameStarted);
        GameStateContext.GameStateMediator.Subscribe(GameEventType.GameFinished, StopTimer);
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
    private void OnDestroy()
    {
        GameStateContext.GameStateMediator.Unsubscribe(GameEventType.GameStarted, OnGameStarted);
        GameStateContext.GameStateMediator.Unsubscribe(GameEventType.GameFinished, StopTimer);
    }
}