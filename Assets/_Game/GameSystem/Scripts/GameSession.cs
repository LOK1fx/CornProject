using LOK1game.Tools;
using UnityEngine;

namespace LOK1game.Game
{
    public abstract class GameSession
    {
        protected GameSession(bool local, bool host, bool server)
        {
            IsLocal = local;
            IsHost = host;
            IsServer = server;
        }

        public float PlayTime { get; private set; }
        public bool IsLocal { get; }
        public bool IsHost { get; }
        public bool IsServer { get; }

        protected abstract void OnInitialized();
        protected abstract void OnEnd();
        protected abstract void OnUpdate();

        public void Initialize()
        {
            CustomUpdate.AddUpdateCallback(Update);
            
            OnInitialized();
        }

        public void End()
        {
            CustomUpdate.RemoveUpdateCallback(Update);
        }

        private void Update()
        {
            UpdateTimer();
            OnUpdate();
        }

        private void UpdateTimer()
        {
            if(IsServer && !IsHost) { return; }

            PlayTime += Time.deltaTime;
        }

        public override string ToString()
        {
            var description = $"Play time: {PlayTime}\n" +
                $"IsLocal: {IsLocal}\n" +
                $"IsHost: {IsHost}\n" +
                $"IsServer: {IsServer}";

            return description;
        }
    }
}