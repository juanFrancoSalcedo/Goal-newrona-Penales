using B_Extensions;
using Services;
using System;

namespace Features.Score
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        public int TotalScore { get; private set; }

        private void OnEnable()
        {
            ScoreMediator.Subscribe(ScoreEventType.ScoreApplied, OnScoreApplied);
            GameStateContext.GameStateMediator.Subscribe(GameEventType.GameStarted, ResetScore);
        }

        private void OnDisable()
        {
            ScoreMediator.Unsubscribe(ScoreEventType.ScoreApplied, OnScoreApplied);
            GameStateContext.GameStateMediator.Unsubscribe(GameEventType.GameStarted, ResetScore);
        }

        private void OnScoreApplied(int score)
        {
            TotalScore += score;
            ScoreMediator.Publish(ScoreEventType.TotalScoreChanged, TotalScore);
        }

        private void ResetScore()
        {
            TotalScore = 0;
            ScoreMediator.Publish(ScoreEventType.TotalScoreChanged, TotalScore);
        }
    }
}
