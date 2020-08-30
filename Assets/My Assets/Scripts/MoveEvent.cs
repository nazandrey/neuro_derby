namespace NeuroDerby.Scripts
{
    public class MoveEvent : IEvent<MoveEventData>
    {
        public PlayerActionsLogging PlayerActionsLogging { set; private get; }

        public void Dispatch(MoveEventData data)
        {
            PlayerActionsLogging?.OnMoveEvent(data);
        }
    }
}