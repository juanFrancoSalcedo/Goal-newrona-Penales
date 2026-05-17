using B_Extensions;
using Features.Score;
using Services;
using System;
using UnityEngine;

namespace Features
{
    public class EndGameManager:Singleton<EndGameManager>
    {
        [SerializeField] private int maxAttempts =3;
        //[SerializeField] private JumpManager jumpManager;
        [SerializeField] private GameObject canvasEnd;
        public static int attempts = 0;

        bool ended = false;
        private void Update()
        {
            if (attempts >= maxAttempts && !ended)
            { 
                ended = true;
                GameStateContext.ChangeState(GameEventType.GameFinished);
                attempts = 0;
                Invoke(nameof(Activefinal), 2f);
            }
        }

        private void Activefinal() 
        {
            canvasEnd.SetActive(true);
        }

        public void UpdateScore() 
        {
            attempts++;
        }
    }
}
