using B_Extensions;
using SFB;
using System.IO;
using UnityEngine;

namespace Services
{
    public class FileSelectorService : Singleton<FileSelectorService>
    {
        private const string DestinationFileName = "texture.png";
        public static event System.Action OnFileLoaded;

        public void OpenFileBrowser()
        {
            var extensions = new ExtensionFilter[]
            {
                new ExtensionFilter("Image Files", "png")
            };

            string[] paths = StandaloneFileBrowser.OpenFilePanel("Seleccionar imagen", "", extensions, false);

            if (paths != null && paths.Length > 0 && !string.IsNullOrEmpty(paths[0]))
            {
                CopyToStreamingAssets(paths[0]);
            }
        }

        private void CopyToStreamingAssets(string sourcePath)
        {
            try
            {
                string destPath = Path.Combine(Application.streamingAssetsPath, DestinationFileName);

                if (!Directory.Exists(Application.streamingAssetsPath))
                {
                    Directory.CreateDirectory(Application.streamingAssetsPath);
                }

                File.Copy(sourcePath, destPath, true);
                OnFileLoaded?.Invoke();
                Debug.Log($"[FileSelectorService] Archivo copiado a: {destPath}");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[FileSelectorService] Error al copiar: {e.Message}");
            }
        }
    }
}