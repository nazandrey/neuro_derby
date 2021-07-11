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
        private const string PlayersFilePath = @"data.json";
        
        private IScoreStorage<TPlayerId, Player> _scoreStorage;

        public PlayersSaver(IScoreStorage<TPlayerId, Player> scoreStorage)
        {
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
            FileSaver.Save<List<Player>, List<PlayerDto>>(PlayersFilePath, _mapperConfig, allPlayers.ToList());
        }
    }
}