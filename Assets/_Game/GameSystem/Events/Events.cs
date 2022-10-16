namespace LOK1game.Game.Events
{
    public static class Events
    {
        
    }

    public class OnGameStateChangedEvent : GameEvent
    {
        public readonly EGameState PreviousState;
        public readonly EGameState NewState;

        public OnGameStateChangedEvent(EGameState previousState, EGameState newState)
        {
            PreviousState = previousState;
            NewState = newState;
        }
    }

    public class OnProjectContextInitializedEvent : SystemEvent
    {
        public readonly ProjectContext ProjectContext;

        public OnProjectContextInitializedEvent(ProjectContext projectContext)
        {
            ProjectContext = projectContext;
        }
    }
}