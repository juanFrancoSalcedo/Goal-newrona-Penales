using UnityEngine;
using UnityEngine.InputSystem;
using Services;

public class ShotManager : MonoBehaviour
{
    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            RaycastManager.Instance.CastRayFromMouse();
        }
    }
}
