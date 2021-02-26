namespace NeuroDerby.Game
{
    public class MoveEvent : IEvent<MoveEventData>
    {
        public PlayerActionsLogging PlayerActionsLogging { set; private get; }

        public void Dispatch(MoveEventData data)
        {
            if (PlayerActionsLogging)
                PlayerActionsLogging.OnMoveEvent(data);
        }
    }
}