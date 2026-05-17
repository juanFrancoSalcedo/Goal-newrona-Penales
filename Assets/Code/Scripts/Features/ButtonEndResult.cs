using B_Extensions;
using UnityEngine;

public class ButtonEndResult : BaseButtonAttendant
{
    void Start()
    {
        buttonComponent.onClick.AddListener(OnEndResult);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && GameStateContext.State == GameEventType.GameFinished)
        {
            Click();
        }
    }

    private void OnEndResult()
    {
    }
}