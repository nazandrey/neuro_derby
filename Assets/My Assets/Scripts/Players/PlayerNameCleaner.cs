namespace NeuroDerby.Players
{
    public class PlayerNameCleaner : IPlayerNameCleaner
    {
        public string Clean(string name) => name.Trim();
    }
}