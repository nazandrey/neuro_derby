namespace NeuroDerby.Players
{
    public class PlayerNameChecker : IPlayerNameChecker
    {
        private const int NameCharsLimit = 25;

        public string GetTooltipTextForInvalidName()
        {
            return $"Name should be not empty and contain less or equal {NameCharsLimit} characters";
        }
        
        public bool Check(string playerName, out string checkedPlayerName)
        {
            if (string.IsNullOrWhiteSpace(playerName))
            {
                checkedPlayerName = playerName;
                return false;
            }
            else
            {
                checkedPlayerName = playerName.Trim();
                if (checkedPlayerName.Length > NameCharsLimit)
                {
                    var shortenedPlayerName = checkedPlayerName.Remove(NameCharsLimit);
                    checkedPlayerName = shortenedPlayerName.Trim();
                }

                return true;
            }
        }
    }
}