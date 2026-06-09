using UnityEngine;
using UnityEngine.UI;

public class IconFootSubmit : MonoBehaviour
{
    [SerializeField] Button btnSubmit;
    [SerializeField] AnimationUIController anima;
    [SerializeField] GameObject staticImage;

    void Update()
    {
        staticImage.SetActive(!btnSubmit.interactable);
        anima.gameObject.SetActive(btnSubmit.interactable);
    }
}
