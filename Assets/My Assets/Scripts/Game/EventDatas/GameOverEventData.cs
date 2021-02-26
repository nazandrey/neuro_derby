namespace NeuroDerby.Game
{
    public class GameOverEventData : IEventData
    {
        public GameOverEventData(int winnerPlayerNum)
        {
            WinnerPlayerNum = winnerPlayerNum;
        }

        public int WinnerPlayerNum { get; private set; }
    }
}