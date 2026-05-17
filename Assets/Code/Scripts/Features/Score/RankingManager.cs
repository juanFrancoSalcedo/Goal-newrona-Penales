using System;
using System.Collections.Generic;
using Services;
using UnityEngine;

namespace Features.Score
{
    public class RankingManager : MonoBehaviour
    {
        [SerializeField] private List<CardRanking> cardRankings = new List<CardRanking>();
        [SerializeField] CardRanking currentPlayerCard;
        [SerializeField] private Timer timer;
        private const int MaxRankingPositions = 5;

        private void OnEnable() => ScoreMediator.Subscribe(ScoreEventType.TotalScoreChanged, OnTotalScoreChanged);
        private void OnDisable() => ScoreMediator.Unsubscribe(ScoreEventType.TotalScoreChanged, OnTotalScoreChanged);

        PlayerData currentPlayer;
        private void OnTotalScoreChanged(int totalScore)
        {
            Invoke(nameof(UpdateRanking),0.1f);
        }

        private void UpdateRanking()
        {
            if (GameStateContext.State != GameEventType.GameFinished)
                return;

            var dd = RandomToken.CreateRandomToken(5);
            var newUid = Guid.NewGuid().ToString();
            PlayerData playerData = new PlayerData(newUid, $"Player {dd}", $"Coreo{dd}@prueba.com", "3124445555", ScoreManager.Instance.TotalScore, timer != null ? timer.GetCurrentTime() : 0f);
            CsvPlayerSaver.Save(playerData);
            currentPlayer = playerData;


            List<PlayerData> players = CsvPlayerSaver.GetSavedPlayers();

            for (int i = 0; i < cardRankings.Count; i++)
            {
                if (i < MaxRankingPositions && i < players.Count)
                { 
                    if (currentPlayer.uid.Equals(players[i].uid))
                        currentPlayerCard.SetData(i+1,currentPlayer.nombre, currentPlayer.score);
                
                    cardRankings[i].SetData(i + 1, players[i].nombre, players[i].score);
                }
                else
                    cardRankings[i].Clear();

            }
        }
    }
}