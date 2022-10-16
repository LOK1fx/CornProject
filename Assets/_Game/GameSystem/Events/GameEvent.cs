using System;

namespace LOK1game.Game.Events
{
    public abstract class Event
    {
        public abstract string SenderName { get; }
    }
    
    public class GameEvent : Event
    {
        private const string SENDER_NAME = "[GAME]";

        public override string SenderName => SENDER_NAME;
    }

    public class SystemEvent : Event
    {
        private const string SENDER_NAME = "[SYSTEM]";

        public override string SenderName => SENDER_NAME;
    }
}