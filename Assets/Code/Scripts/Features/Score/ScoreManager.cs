using B_Extensions;
using Services;
using System;
using UnityEngine;

namespace Features.Score
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        public int TotalScore { get; private set; }

        private void OnEnable() => ScoreMediator.Subscribe(ScoreEventType.ScoreApplied, OnScoreApplied);
        private void OnDisable() => ScoreMediator.Unsubscribe(ScoreEventType.ScoreApplied, OnScoreApplied);

        private void OnScoreApplied(int score)
        {
            TotalScore += score;
            ScoreMediator.Publish(ScoreEventType.TotalScoreChanged, TotalScore);
        }
    }
}
