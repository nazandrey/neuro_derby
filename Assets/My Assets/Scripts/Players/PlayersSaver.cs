using System.Collections.Generic;
using System.IO;
using System.Linq;
using NeuroDerby.FileOperations;
using NeuroDerby.RatingSystem;
using NeuroDerby.RatingSystem.Glicko;
using UnityEngine;

namespace NeuroDerby.Players
{
    public class PlayersSaver<TPlayerId> : IPlayersSaver
    {
        private IScoreStorage<TPlayerId, Player> _scoreStorage;
        private PathConfig _pathConfig;
        private IConverter<List<Player>, List<PlayerDto>> _converter;

        public PlayersSaver(IScoreStorage<TPlayerId, Player> scoreStorage, PathConfig pathConfig, 
            IConverter<List<Player>, List<PlayerDto>> converter)
        {
            _pathConfig = pathConfig;
            _scoreStorage = scoreStorage;
            _converter = converter;
        }

        public void Save()
        {
            var allPlayers = _scoreStorage.GetAllScores();
            FileSaver.Save<List<Player>, List<PlayerDto>>(Path.Combine(Application.persistentDataPath, _pathConfig.PersistentPlayerDataPathPostfix), _converter, allPlayers.ToList());
        }
    }
}