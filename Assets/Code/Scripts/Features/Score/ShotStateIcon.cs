using Features.Score;
using System;
using UnityEngine;

namespace Features
{
    [Serializable]
    public class ShotStateMapping
    {
        public TypeShot typeShot;
        public GameObject iconObject;
    }

    public class ShotStateIcon : MonoBehaviour
    {
        [SerializeField] private ShotStateMapping[] mappings;
        [SerializeField] private TypeShot currentState = TypeShot.None;
        [SerializeField] private int attempt = 0;

        private void OnEnable()
        {
            SetState(currentState);
            ShotMediator.Subscribe(ShotEventType.ShotApplied, SetState);
        }

        private void OnDisable()
        {
            ShotMediator.Unsubscribe(ShotEventType.ShotApplied, SetState);
        }

        public void SetState(TypeShot typeShot)
        {
            currentState = typeShot;

            if (EndGameManager.Instance.attempts != attempt)
                return;

            foreach (var mapping in mappings)
            {
                if (mapping.iconObject != null)
                {
                    mapping.iconObject.SetActive(mapping.typeShot == typeShot);
                }
            }
        }
    }
}