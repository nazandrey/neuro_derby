namespace NeuroDerby.RatingSystem.Glicko
{
    public class PlayerDto
    {
        public string Name { get; set; }
        public double Rating { get; set; }
        public double Deviation { get; set; }
        public double Volatility { get; set; }
    }
}