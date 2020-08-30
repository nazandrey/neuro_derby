namespace NeuroDerby.Scripts
{
    public class MoveEventData : IEventData
    {
        public int PlayerNum { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float HDirection { get; set; }
        public float VDirection { get; set; }
    }
}