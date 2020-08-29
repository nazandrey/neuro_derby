using Glicko2;

namespace Scripts.RatingSystem.Glicko
{
    public class Player
    {
        public Player(Rating rating, string playerName)
        {
            RatingInfo = rating;
            Name = playerName;
        }

        public Player(RatingCalculator calculator, PlayerDto savedPlayer)
        {
            RatingInfo = new Rating(calculator, savedPlayer.Rating, savedPlayer.Deviation, savedPlayer.Volatility);
            Name = savedPlayer.Name;
        }

        public string Name { get; }
        public Rating RatingInfo { get; }
        public double Rating => RatingInfo.GetRating();
        public double Deviation => RatingInfo.GetRatingDeviation();
        public double Volatility => RatingInfo.GetVolatility();
    }
}