interface IEvent<in TData> where TData : IEventData
{
    void Dispatch(TData data = default(TData));
}
