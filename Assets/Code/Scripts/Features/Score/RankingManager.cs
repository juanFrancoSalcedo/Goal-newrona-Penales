using System;
using System.Collections.Generic;
using B_Extensions;
using Services;
using UnityEngine;

namespace Features.Score
{
    public class RankingManager : Singleton<RankingManager>
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

        public void RegisterCurrentPlayer(string nombre, string correo, string telefono)
        {
            var uid = Guid.NewGuid().ToString();
            float time = timer != null ? timer.GetCurrentTime() : 0f;
            currentPlayer = new PlayerData(uid, nombre, correo, telefono, 0, time);

        }

        private void UpdateRanking()
        {
            //UpdateRanking();
            currentPlayer.score = ScoreManager.Instance.TotalScore;

            if (GameStateContext.State != GameEventType.GameFinished)
            {
                print("RankingManager: Ignoring ranking update because the game is not finished.");
                return;
            }
            CsvPlayerSaver.Save(currentPlayer);

            List<PlayerData> players = CsvPlayerSaver.GetSavedPlayers();

            for (int i = 0; i < cardRankings.Count; i++)
            {
                if (i < MaxRankingPositions && i < players.Count)
                { 
                    if (currentPlayer != null && currentPlayer.uid.Equals(players[i].uid))
                        currentPlayerCard.SetData(i+1,currentPlayer.nombre, currentPlayer.score);
                
                    cardRankings[i].SetData(i + 1, players[i].nombre, players[i].score);
                }
                else
                    cardRankings[i].Clear();

            }
        }
    }
}