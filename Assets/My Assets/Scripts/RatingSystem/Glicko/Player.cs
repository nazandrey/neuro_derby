using System;
using Glicko2;

namespace NeuroDerby.RatingSystem.Glicko
{
    public class Player : IComparable<Player>
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

        public int CompareTo(Player other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;

            return Rating.CompareTo(other.Rating);
        }
    }
}