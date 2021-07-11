using System.Collections.Generic;
using NeuroDerby.RatingSystem.Glicko;

namespace NeuroDerby.Players
{
    public interface IPlayersDtoLoader
    {
        List<PlayerDto> Load();
    }
}