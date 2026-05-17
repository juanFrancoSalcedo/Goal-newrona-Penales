using Services;
using System;
using System.IO;
using UnityEngine;

namespace Utils
{
    public class TextureLoader : MonoBehaviour, IAdminListener
    {
        [SerializeField] private Material targetMaterial;
        [SerializeField] private Renderer targetRenderer;
        [SerializeField] private string textureFileName = "texture.png";
        [SerializeField] private string textureProperty = "_MainTex";
        [SerializeField] private bool loadOnStart = true;
        [SerializeField] private bool useMaterialPropertyBlock = true;

        private MaterialPropertyBlock _propertyBlock;

        private void Start()
        {
            if (loadOnStart)
            {
                LoadAndApplyTexture();
            }
        }

        public void LoadAndApplyTexture()
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, textureFileName);

            if (!File.Exists(filePath))
            {
                Debug.LogWarning($"[TextureLoader] File not found: {filePath}");
                return;
            }

            byte[] data = File.ReadAllBytes(filePath);
            Texture2D texture = new Texture2D(2, 2);
            if (!texture.LoadImage(data))
            {
                Debug.LogWarning($"[TextureLoader] Failed to load image: {filePath}");
                Destroy(texture);
                return;
            }

            ApplyTexture(texture);
        }

        public void LoadAndApplyTexture(string fileName)
        {
            textureFileName = fileName;
            LoadAndApplyTexture();
        }

        public void UpdateBehaviour()
        {
            LoadAndApplyTexture();
        }

        private void ApplyTexture(Texture2D texture)
        {
            if (targetMaterial != null)
            {
                targetMaterial.SetTexture(textureProperty, texture);
                return;
            }

            if (targetRenderer != null)
            {
                if (useMaterialPropertyBlock)
                {
                    _propertyBlock ??= new MaterialPropertyBlock();
                    targetRenderer.GetPropertyBlock(_propertyBlock);
                    _propertyBlock.SetTexture(textureProperty, texture);
                    targetRenderer.SetPropertyBlock(_propertyBlock);
                }
                else
                {
                    targetRenderer.material.SetTexture(textureProperty, texture);
                }
            }
        }
    }
}
