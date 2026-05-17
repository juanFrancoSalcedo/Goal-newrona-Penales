using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdminPassword : MonoBehaviour
{
    private const string Password = "NWG";

    [SerializeField] private GameObject adminPanel;
    [SerializeField] private GameObject passwordPanel;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button verifyButton;

    private void Start()
    {
        verifyButton.onClick.AddListener(VerifyPassword);
    }

    private void VerifyPassword()
    {
        if (inputField.text.ToUpper() == Password)
        {
            adminPanel.SetActive(true);
            passwordPanel.SetActive(false);
            Debug.Log("Admin access granted.");
            inputField.text = string.Empty; // Clear input for security
        }
        else
        {
            Debug.Log("Incorrect password.");
        }
    }
}