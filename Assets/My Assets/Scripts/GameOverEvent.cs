namespace NeuroDerby
{
    public class GameOverEvent : IEvent<EmptyEventData>
    {
        public EntryPoint EntryPoint { private get; set; }

        public void Dispatch(EmptyEventData data = null)
        {
            EntryPoint?.OnGameOver();
        }
    }
}