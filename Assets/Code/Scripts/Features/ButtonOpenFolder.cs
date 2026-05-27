using B_Extensions;
using System.Diagnostics;
using UnityEngine;

namespace Features
{
    public class ButtonOpenFolder : BaseButtonAttendant
    {
        [SerializeField] private string folderPath = "";

        private void Start() => buttonComponent.onClick.AddListener(OnButtonClick);

        private void OnButtonClick()
        {
            string path = string.IsNullOrEmpty(folderPath) ? Application.persistentDataPath : folderPath;
            Process.Start("explorer.exe", path.Replace('/', '\\'));
        }
    }
}