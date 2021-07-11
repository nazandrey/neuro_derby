namespace NeuroDerby.Players
{
    public class PlayerNumToNameConverter : IPlayerNumToIdConverter<string>
    {
        public string Get(int playerNum)
        {
            return GameState.GetPlayerNameByNum(playerNum);
        }
    }
}