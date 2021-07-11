using Glicko2;
using NeuroDerby.Players;

namespace NeuroDerby.RatingSystem.Glicko
{
    public static class GlickoScoreUpdaterBuilder
    {
        public static GlickoScoreUpdater<string> Build(IScoreStorage<string, Player> scoreStorage)
        {
            return new GlickoScoreUpdater<string>(scoreStorage, new PlayerNumToNameConverter(),
                new PlayersSaver<string>(scoreStorage), new RatingCalculator());
        } 
    }
}