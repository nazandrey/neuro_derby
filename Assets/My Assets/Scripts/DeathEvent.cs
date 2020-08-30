namespace NeuroDerby
{
    public class DeathEvent : IEvent<EmptyEventData>
    {
        public GameOverHandler GameOverHandler { set; private get; }

        public void Dispatch(EmptyEventData data = null)
        {
            GameOverHandler?.OnDeathEvent();
        }
    }
}