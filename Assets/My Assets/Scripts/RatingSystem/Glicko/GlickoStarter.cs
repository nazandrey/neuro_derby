using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Glicko2;
using NeuroDerby.FileOperations;
using UnityEngine;

namespace NeuroDerby.RatingSystem.Glicko
{
    public class GlickoStarter : MonoBehaviour
    {
        private const string SaveFilePath = @"data.json";

        private void Start()
        {
            var calculator = new RatingCalculator();

            var players = new List<Player>();
            var savedPlayers = FileLoader.Load<List<PlayerDto>>(SaveFilePath);
            if (savedPlayers != null)
                players = savedPlayers.Select(savedPlayer => new Player(calculator, savedPlayer)).ToList();

            if (players.Count == 0)
            {
                players.Add(new Player(new Rating(calculator), "player1"));
                players.Add(new Player(new Rating(calculator), "player2"));
                players.Add(new Player(new Rating(calculator), "player3"));
            }

            ShowPlayersInConsole(players);

            var results = new RatingPeriodResults();
            for (var i = 0; i < 20; i++)
            {
                results.AddResult(players[0].RatingInfo, players[1].RatingInfo);
            }

            for (var i = 0; i < 40; i++)
            {
                results.AddResult(players[0].RatingInfo, players[2].RatingInfo);
            }

            calculator.UpdateRatings(results);

            ShowPlayersInConsole(players);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddCollectionMappers();
                cfg.CreateMap<PlayerDto, Player>().EqualityComparison((pdto, p) => pdto.Name == p.Name);
                cfg.CreateMap<Player, PlayerDto>().EqualityComparison((p, pdto) => p.Name == pdto.Name);
            });
            FileSaver.Save<List<Player>, List<PlayerDto>>(SaveFilePath, config, players);
        }

        private static void ShowPlayersInConsole(IEnumerable<Player> players)
        {
            foreach (var player in players)
            {
                Debug.Log("Player " + player.Name + " values: " + player.Rating + ", " +
                                  player.Deviation + ", " + player.Volatility);
            }
        }
    }
}