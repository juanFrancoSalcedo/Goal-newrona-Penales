using B_Extensions;
using Services;
using UnityEngine;

namespace Features
{
    public class ButtonSelectFile : BaseButtonAttendant
    {
        void Start() => buttonComponent.onClick.AddListener(OnButtonClick);

        private void OnButtonClick() => FileSelectorService.Instance.OpenFileBrowser();
    }
}