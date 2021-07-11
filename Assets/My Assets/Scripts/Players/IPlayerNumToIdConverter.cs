namespace NeuroDerby.Players
{
    public interface IPlayerNumToIdConverter<TPlayerId>
    {
        TPlayerId Get(int playerNum);
    }
}