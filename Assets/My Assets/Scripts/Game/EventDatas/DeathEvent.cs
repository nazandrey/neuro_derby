﻿namespace NeuroDerby.Game
{
    public class DeathEvent : IEvent<EmptyEventData>
    {
        public GameOverHandler GameOverHandler { set; private get; }

        public void Dispatch(EmptyEventData data = null)
        {
            if (GameOverHandler)
                GameOverHandler.OnDeathEvent();
        }
    }
}