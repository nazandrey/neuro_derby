using System.Collections.Generic;

namespace NeuroDerby.Core
{
    public static class GameState
    {
        private static readonly Dictionary<int, string> CurrentPlayerNames = new Dictionary<int, string>();

        public static void ClearPlayerNames()
        {
            CurrentPlayerNames.Clear();
        }

        public static void AddPlayer(int num, string name)
        {
            CurrentPlayerNames.Add(num, name);
        }
    }
}