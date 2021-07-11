namespace NeuroDerby.Game.EventDatas
{
    public class GameOverEventData : IEventData
    {
        public GameOverEventData(bool isDraw, int winnerPlayerNum, int loserPlayerNum)
        {
            IsDraw = isDraw;
            WinnerPlayerNum = winnerPlayerNum;
            LoserPlayerNum = loserPlayerNum;
        }

        public bool IsDraw { get; }
        public int WinnerPlayerNum { get; }
        public int LoserPlayerNum { get; }
    }
}