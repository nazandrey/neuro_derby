using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using NeuroDerby.FileOperations;
using NeuroDerby.RatingSystem;
using NeuroDerby.RatingSystem.Glicko;

namespace NeuroDerby.Players
{
    public class PlayersSaver<TPlayerId> : IPlayersSaver
    {
        private IScoreStorage<TPlayerId, Player> _scoreStorage;
        private PathConfig _pathConfig;

        public PlayersSaver(IScoreStorage<TPlayerId, Player> scoreStorage, PathConfig pathConfig)
        {
            _pathConfig = pathConfig;
            _scoreStorage = scoreStorage;
        }

        private static readonly MapperConfiguration _mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddCollectionMappers();
            cfg.CreateMap<PlayerDto, Player>().EqualityComparison((pdto, p) => pdto.Name == p.Name);
            cfg.CreateMap<Player, PlayerDto>().EqualityComparison((p, pdto) => p.Name == pdto.Name);
        });

        public void Save()
        {
            var allPlayers = _scoreStorage.GetAllScores();
            FileSaver.Save<List<Player>, List<PlayerDto>>(_pathConfig.PersistentPlayerDataPathPostfix, _mapperConfig, allPlayers.ToList());
        }
    }
}