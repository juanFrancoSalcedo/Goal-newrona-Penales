using B_Extensions;
using System;
using UnityEngine;

public class ButtonSubmitForm : BaseButtonAttendant, IFormSubmitable
{
    public Action OnPass { get; set; }

    private void Start()
    {
        buttonComponent.onClick.AddListener(Submit);
    }

    private void Submit()
    {
        GameStateContext.ChangeState(GameEventType.FormSubmitted);
        OnPass?.Invoke();
    }

    public void EnableSubmit(bool enable)
    {
        buttonComponent.interactable = enable;
    }
}
