using System.Collections.Generic;
using NeuroDerby.FileOperations;
using NeuroDerby.RatingSystem.Glicko;

namespace NeuroDerby.Players
{
    public class PlayersDtoLoader : IPlayersDtoLoader
    {
        private readonly PathConfig _pathConfig;

        public PlayersDtoLoader(PathConfig pathConfig)
        {
            _pathConfig = pathConfig;
        }
        
        public List<PlayerDto> Load()
        {
            return FileLoader.Load<List<PlayerDto>>(_pathConfig.PersistentPlayerDataPathPostfix);
        }
    }
}