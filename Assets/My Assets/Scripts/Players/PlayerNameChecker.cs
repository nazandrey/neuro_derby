namespace NeuroDerby.Players
{
    public class PlayerNameChecker : IPlayerNameChecker
    {
        private const int NameCharsLimit = 25;

        public string GetTooltipTextForInvalidName() =>
            $"Name should be not empty, contain less or equal {NameCharsLimit} characters";

        public bool Check(string playerName) =>
            !string.IsNullOrWhiteSpace(playerName) 
            && !(playerName.Length > NameCharsLimit);
    }
}