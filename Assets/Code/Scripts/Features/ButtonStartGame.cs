using B_Extensions;
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
        Invoke(nameof(NextPos),5f);
    }

    private void NextPos() 
    {
        GameStateContext.ChangeState(GameEventType.GameStarted);
    }
}
