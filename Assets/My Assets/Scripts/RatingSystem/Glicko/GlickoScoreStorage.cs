using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Glicko2;
using NeuroDerby.FileOperations;
using UnityEngine;

namespace NeuroDerby.RatingSystem.Glicko
{
    public class GlickoScoreStorage : SingletonUndestroyable<GlickoScoreStorage>, IScoreStorage<string, double>
    {
        private const string PlayersFilePath = @"data.json";
        
        private RatingCalculator _calculator = new RatingCalculator();
        private List<Player> _players;
        private ScoreStorage<string, double> _playerScoreStorage = new ScoreStorage<string, double>();

        protected override void Awake()
        {
            base.Awake();
            LoadPlayers();
        }

        public void UpdateScoreThroughWin(string winnerName, string loserName)
        {
            UpdateScore(winnerName, loserName, false);
        }
        
        public void UpdateScoreThroughDraw(string firstName, string secondName)
        {
            UpdateScore(firstName, secondName, true);
        }

        public IEnumerable<KeyValuePair<string, double>> GetAllScoresWithId()
        {
            return _playerScoreStorage.GetAllScoresWithId();
        }

        private void UpdateScore(string firstName, string secondName, bool isDraw)
        {
            var firstRatingInfo = GetRatingInfoByName(firstName);
            var secondRatingInfo = GetRatingInfoByName(secondName);
            if (firstRatingInfo == null || secondRatingInfo == null)
            {
                Debug.Log($"Rating info not found for {firstName} or {secondName}");
                return;
            }

            var results = new RatingPeriodResults();
            if(isDraw)
                results.AddDraw(firstRatingInfo, secondRatingInfo);
            else
                results.AddResult(firstRatingInfo, secondRatingInfo);
            _calculator.UpdateRatings(results);
            
            SavePlayers(_players);
        }

        private Rating GetRatingInfoByName(string winnerName)
        {
            return _players.FirstOrDefault(x => x.Name == winnerName)?.RatingInfo;
        }

        private void LoadPlayers()
        {
            _players = new List<Player>();
            var savedPlayers = FileLoader.Load<List<PlayerDto>>(PlayersFilePath);
            if (savedPlayers != null)
                _players = savedPlayers.Select(savedPlayer => new Player(_calculator, savedPlayer)).ToList();

            foreach (var player in _players)
                _playerScoreStorage.TryAddScore(player.Name, player.Rating);
        }

        private static readonly MapperConfiguration _mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddCollectionMappers();
            cfg.CreateMap<PlayerDto, Player>().EqualityComparison((pdto, p) => pdto.Name == p.Name);
            cfg.CreateMap<Player, PlayerDto>().EqualityComparison((p, pdto) => p.Name == pdto.Name);
        });
        
        private static void SavePlayers(List<Player> players)
        {
            FileSaver.Save<List<Player>, List<PlayerDto>>(PlayersFilePath, _mapperConfig, players);
        }
    }
}