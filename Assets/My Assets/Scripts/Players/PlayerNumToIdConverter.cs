using Zenject;

namespace NeuroDerby.Players
{
    public class PlayerNumToNameConverter : IPlayerNumToIdConverter<string>
    {
        private GameState _gameState;
        
        [Inject]
        private void Construct(GameState gameState)
        {
            _gameState = gameState;
        }
        
        public string Get(int playerNum)
        {
            return _gameState.GetPlayerNameByNum(playerNum);
        }
    }
}