using LOK1game.Game;

namespace LOK1game.Tools
{
    public class GameObjectActivitySetterOnPauseState : GameObjectActivitySetterBase
    {
        protected override void OnGameStateChanged(EGameState newGameState)
        {
            if(newGameState != EGameState.Paused) { return; }

            targetGameObject.SetActive(activateObject);
        }
    }
}