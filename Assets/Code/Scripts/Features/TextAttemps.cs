using Features;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TextAttemps : MonoBehaviour
{
    [SerializeField] private string format = "Attempts: {0}";

    TMP_Text textCompo;

    private void Awake()
    {
        textCompo = GetComponent<TMP_Text>();
    }

    void Update()
    {
        textCompo.text = string.Format(format, EndGameManager.Instance.attempts);
    }
}

