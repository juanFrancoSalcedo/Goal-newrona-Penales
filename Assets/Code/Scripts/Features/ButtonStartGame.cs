using B_Extensions;
using Services;
using System;
using UnityEngine;

public class ButtonStartGame : BaseButtonAttendant
{
    void Start()
    {
        buttonComponent.onClick.AddListener(StartGame);        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            buttonComponent.onClick.Invoke();
        }
    }

    private void StartGame()
    {
        GameStateContext.ChangeState(GameEventType.IntroCountDown);
        Invoke(nameof(NextPos),CountdownDataService.LoadDuration());
    }

    private void NextPos() 
    {
        GameStateContext.ChangeState(GameEventType.GameStarted);
    }
}
