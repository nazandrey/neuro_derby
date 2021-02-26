namespace NeuroDerby.Game
{
    public class GameOverEvent : IEvent<GameOverEventData>
    {
        public EntryPoint EntryPoint { private get; set; }

        public void Dispatch(GameOverEventData data = null)
        {
            if (EntryPoint)
                EntryPoint.OnGameOver();
        }
    }
}