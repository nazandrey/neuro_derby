using NeuroDerby.Players;
using Zenject;

namespace NeuroDerby.MenuLogic
{
    public class EntryPoint : IInitializable
    {
        private IPlayersLoader _playersLoader;

        public EntryPoint(IPlayersLoader playersLoader)
        {
            _playersLoader = playersLoader;
        }

        public void Initialize()
        {
            _playersLoader.Load();
        }
    }
}