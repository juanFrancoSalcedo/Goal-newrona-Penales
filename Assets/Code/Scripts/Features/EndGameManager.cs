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
        public int attempts = 0;
        public event Action<int> OnGameAttempsChanged;
        public event Action OnGameEnd;
        bool ended = false;


        private void OnEnable()
        {
            GameStateContext.GameStateMediator.Subscribe(GameEventType.FormSubmitted, ResetAttempts);
            GameStateContext.GameStateMediator.Subscribe(GameEventType.GameStarted, ResetEnded);
        }

        private void OnDisable()
        {
            GameStateContext.GameStateMediator.Unsubscribe(GameEventType.FormSubmitted, ResetAttempts);
            GameStateContext.GameStateMediator.Unsubscribe(GameEventType.GameStarted, ResetEnded);
        }

        private void ResetEnded() => ended = false;

        private void Update()
        {
            if (attempts >= maxAttempts && !ended)
            { 
                ended = true;
                GameStateContext.ChangeState(GameEventType.GameFinished);
                Invoke(nameof(Activefinal), 2f);
            }
        }

        private void Activefinal() => OnGameEnd?.Invoke();

        public void SetfullAttempts() 
        {
            attempts = maxAttempts;
            OnGameAttempsChanged?.Invoke(attempts);
        }

        public void ResetAttempts() 
        {
            attempts = 0;
        }
        public void UpdateScore() 
        {
            attempts++;
            OnGameAttempsChanged?.Invoke(attempts);
        }
    }
}
