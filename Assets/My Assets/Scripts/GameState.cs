using System.Collections.Generic;

namespace NeuroDerby
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
        
        public static string GetPlayerNameByNum(int num)
        {
            if (CurrentPlayerNames.TryGetValue(num, out var name))
                return name;
            else
                return null;
        }
    }
}