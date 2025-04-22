using System.Collections.Generic;

namespace NeuroDerby
{
    public class GameState
    {
        private readonly Dictionary<int, string> CurrentPlayerNames = new Dictionary<int, string>();

        public bool IsOver { get; private set; }

        public void SetGameIsOver(bool isOver)
        {
            IsOver = isOver;
        }
        
        public void ClearPlayerNames()
        {
            CurrentPlayerNames.Clear();
        }

        public void AddPlayer(int num, string name)
        {
            CurrentPlayerNames.Add(num, name);
        }
        
        public string GetPlayerNameByNum(int num)
        {
            if (CurrentPlayerNames.TryGetValue(num, out var name))
                return name;
            else
                return null;
        }
    }
}