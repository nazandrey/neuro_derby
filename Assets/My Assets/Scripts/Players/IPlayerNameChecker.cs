namespace NeuroDerby.Players
{
    public interface IPlayerNameChecker
    {
        bool Check(string playerName, out string checkedPlayerName);
    }
}