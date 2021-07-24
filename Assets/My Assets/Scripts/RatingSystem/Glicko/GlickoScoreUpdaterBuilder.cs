using Glicko2;
using NeuroDerby.Players;

namespace NeuroDerby.RatingSystem.Glicko
{
    public static class GlickoScoreUpdaterBuilder
    {
        public static GlickoScoreUpdater Build(IScoreStorage<string, Player> scoreStorage)
        {
            return new GlickoScoreUpdater(scoreStorage, new PlayerNumToNameConverter(), 
                new PlayersSaver<string>(scoreStorage), new PlayerNameChecker(), new RatingCalculator());
        } 
    }
}