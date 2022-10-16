using LOK1game.Game.Events;

namespace LOK1game.Game
{
    public enum EGameState
    {
        Gameplay,
        Paused
    }

    public class GameStateManager
    {
        public EGameState CurrentGameState { get; private set; }

        public delegate void GameStateChangeHandler(EGameState newGameState);
        public event GameStateChangeHandler OnGameStateChanged;

        public void SetState(EGameState gameState)
        {
            if (gameState == CurrentGameState)
                return;

            var evt = new OnGameStateChangedEvent(CurrentGameState, gameState);
            
            EventManager.Broadcast(evt);

            CurrentGameState = gameState;
            OnGameStateChanged?.Invoke(CurrentGameState);
        }
    }
}