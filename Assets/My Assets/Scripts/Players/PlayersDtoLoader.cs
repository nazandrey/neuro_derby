using System.Collections.Generic;
using NeuroDerby.FileOperations;
using NeuroDerby.RatingSystem.Glicko;

namespace NeuroDerby.Players
{
    public class PlayersDtoLoader : IPlayersDtoLoader
    {
        private const string PlayersFilePath = @"data.json";
        
        public List<PlayerDto> Load()
        {
            return FileLoader.Load<List<PlayerDto>>(PlayersFilePath);
        }
    }
}