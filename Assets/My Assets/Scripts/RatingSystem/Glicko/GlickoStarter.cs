using System.Collections.Generic;
using System.Linq;
using Glicko2;
using NeuroDerby.FileOperations;
using UnityEngine;

namespace NeuroDerby.RatingSystem.Glicko
{
    public class GlickoStarter : MonoBehaviour
    {
        private const string SaveFilePath = @"data.json";
       

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