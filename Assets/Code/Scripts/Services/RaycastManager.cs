using UnityEngine;
using UnityEngine.InputSystem;
using B_Extensions;
using Features.Score;

namespace Services
{
    public class RaycastManager : Singleton<RaycastManager>
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private float rayDistance = 100f;
        [SerializeField] private LayerMask receptorLayer;

        private new void Awake()
        {
            base.Awake();
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }
        }

        public void CastRay(Vector2 screenPosition)
        {
            if (mainCamera == null) { return; }

            Ray ray = mainCamera.ScreenPointToRay(screenPosition);
            Vector3 endPoint = ray.GetPoint(rayDistance);

            if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, receptorLayer))
            {
                endPoint = hit.point;
                IScoreReceptor receptor = hit.collider.GetComponent<IScoreReceptor>();
                if (receptor != null)
                {
                    receptor.ApplyScore(hit.point);
                }
            }

            Debug.DrawRay(ray.origin, endPoint - ray.origin, Color.blue, 0.1f);
        }

        public void CastRayFromMouse()
        {
            if (Mouse.current == null) { return; }
            CastRay(Mouse.current.position.ReadValue());
        }
    }
}