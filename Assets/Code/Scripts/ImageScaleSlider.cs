using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Features
{
    public class ImageScaleSlider : MonoBehaviour
    {
        [SerializeField] private Slider scaleSlider;
        [SerializeField] private UIImageLoader imageLoader;
        [SerializeField] private float minScale = 0.5f;
        [SerializeField] private float maxScale = 2f;

        private void Start()
        {
            if (scaleSlider == null || imageLoader == null)
            {
                Debug.LogWarning("[ImageScaleSlider] Slider or UIImageLoader reference is missing.");
                return;
            }

            scaleSlider.onValueChanged.AddListener(OnSliderChanged);
            float savedScale = PlayerPrefs.GetFloat(KeyStorage.ImageScale, 1f);
            scaleSlider.value = Mathf.InverseLerp(minScale, maxScale, savedScale);
            ApplyScale(savedScale);
        }

        private void OnSliderChanged(float value)
        {
            float scale = Mathf.Lerp(minScale, maxScale, value);
            PlayerPrefs.SetFloat(KeyStorage.ImageScale, scale);
            PlayerPrefs.Save();
            ApplyScale(scale);
        }

        private void ApplyScale(float scale)
        {
            RectTransform imageTransform = imageLoader.ImageTransform;
            if (imageTransform != null)
                imageTransform.localScale = Vector3.one * scale;
            AdminManager.Instance.NotifyAll();
        }

        private void OnDestroy()
        {
            if (scaleSlider != null)
                scaleSlider.onValueChanged.RemoveListener(OnSliderChanged);
        }
    }
}