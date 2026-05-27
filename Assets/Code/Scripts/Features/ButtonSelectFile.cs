using B_Extensions;
using Services;
using UnityEngine;

namespace Features
{
    public class ButtonSelectFile : BaseButtonAttendant
    {
        [SerializeField] private string fileName = "texture.png";

        void Start() => buttonComponent.onClick.AddListener(OnButtonClick);

        private void OnButtonClick() => FileSelectorService.Instance.OpenFileBrowser(fileName);
    }
}