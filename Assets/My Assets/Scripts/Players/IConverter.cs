namespace NeuroDerby.Players
{
    public interface IConverter<TInput, TOutput>
    {
        public TOutput Convert(TInput input);
    }
}