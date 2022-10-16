using LOK1game.Game;

namespace LOK1game.Tools
{
    public class GameObjectActivitySetterOnGameplayState : GameObjectActivitySetterBase
    {
        protected override void OnGameStateChanged(EGameState newGameState)
        {
            if (newGameState != EGameState.Gameplay) { return; }

            targetGameObject.SetActive(activateObject);
        }
    }

}