using System;

namespace LOK1game.DebugTools
{
    public abstract class DebugConsoleCommandBase
    {
        public string Id { get; }
        public string Description { get; }
        public string Format { get; }

        protected DebugConsoleCommandBase(string id, string description, string format)
        {
            Id = id;
            Description = description;
            Format = format;
        }

        public abstract void Invoke();
    }

    public class DebugConsoleCommand : DebugConsoleCommandBase
    {
        private readonly Action _callback;
        
        public DebugConsoleCommand(string id, string description, string format, Action command) : base(id, description,
            format)
        {
            _callback = command;
        }

        public override void Invoke()
        {
            _callback.Invoke();
        }
    }
}