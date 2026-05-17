using Services;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    public class UIImageLoader : MonoBehaviour, IAdminListener
    {
        [SerializeField] private Image targetImage;
        [SerializeField] private string textureFileName = "texture.png";
        [SerializeField] private bool loadOnStart = true;

        private void Start()
        {
            if (loadOnStart)
                LoadAndApplyImage();
        }

        public void LoadAndApplyImage()
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, textureFileName);

            if (!File.Exists(filePath))
            {
                Debug.LogWarning($"[UIImageLoader] File not found: {filePath}");
                return;
            }

            byte[] data = File.ReadAllBytes(filePath);
            Texture2D texture = new Texture2D(2, 2);
            if (!texture.LoadImage(data))
            {
                Debug.LogWarning($"[UIImageLoader] Failed to load image: {filePath}");
                Destroy(texture);
                return;
            }

            ApplyImage(texture);
        }

        public void LoadAndApplyImage(string fileName)
        {
            textureFileName = fileName;
            LoadAndApplyImage();
        }

        public void UpdateBehaviour()
        {
            LoadAndApplyImage();
        }

        private void ApplyImage(Texture2D texture)
        {
            if (targetImage != null)
                targetImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
        }
    }
}