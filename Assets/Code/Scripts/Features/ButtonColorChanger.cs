using B_Extensions;
using Features;
using UnityEngine;
using Utils;

namespace Features
{
    public class ButtonColorChanger : BaseButtonAttendant
    {
        [SerializeField] private MaterialColorChanger targetColorChanger;
        [SerializeField] private Color buttonColor = Color.white;

        private void Start() => buttonComponent.onClick.AddListener(OnButtonClick);

        private void OnButtonClick()
        {
            PlayerPrefs.SetString(KeyStorage.GoalkeeperColor, ColorUtility.ToHtmlStringRGBA(buttonColor));
            targetColorChanger.SetColor(buttonColor);
            AdminManager.Instance.NotifyAll();
        }
    }
}