using Services;
using UnityEngine;

namespace Utils
{
    public class MaterialColorChanger : MonoBehaviour, IAdminListener
    {
        [SerializeField] private Renderer targetRenderer;
        [SerializeField] private Color targetColor = Color.white;
        [SerializeField] private string colorProperty = "_BaseColor";

        private MaterialPropertyBlock _propertyBlock;
        private static readonly int ColorProperty = Shader.PropertyToID("_BaseColor");

        private void OnEnable()
        {
            if (PlayerPrefs.HasKey(KeyStorage.GoalkeeperColor))
            {
                if (ColorUtility.TryParseHtmlString("#" + PlayerPrefs.GetString(KeyStorage.GoalkeeperColor), out Color savedColor))
                    targetColor = savedColor;
            }
            ApplyColor();
        }

        public void SetColor(Color color)
        {
            targetColor = color;
            ApplyColor();
        }

        public void UpdateBehaviour()
        {
            ApplyColor();
        }

        private void ApplyColor()
        {
            if (targetRenderer == null) return;

            _propertyBlock ??= new MaterialPropertyBlock();
            targetRenderer.GetPropertyBlock(_propertyBlock);
            _propertyBlock.SetColor(colorProperty, targetColor);
            targetRenderer.SetPropertyBlock(_propertyBlock);
        }
    }
}