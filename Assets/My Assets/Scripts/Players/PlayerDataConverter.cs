using System.Collections.Generic;
using System.Linq;
using NeuroDerby.RatingSystem.Glicko;

namespace NeuroDerby.Players
{
    public class PlayerDataConverter : IConverter<List<Player>, List<PlayerDto>>
    {
        public List<PlayerDto> Convert(List<Player> input)
        {
            return input.Select(x => new PlayerDto
            {
                Name = x.Name,
                Rating = x.Rating,
                Deviation = x.Deviation,
                Volatility = x.Volatility
            }).ToList();
        }
    }
}