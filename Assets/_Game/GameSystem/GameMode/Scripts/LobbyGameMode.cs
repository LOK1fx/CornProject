using System.Collections;

namespace LOK1game.Game
{
    public class LobbyGameMode : BaseGameMode, IGameMode
    {
        public override EGameModeId Id => EGameModeId.Lobby;

        public override IEnumerator OnStart()
        {
            State = EGameModeState.Starting;

            RegisterGameModeObject(Instantiate(CameraPrefab));
            RegisterGameModeObject(Instantiate(UiPrefab));

            State = EGameModeState.Started;

            yield return null;
        }

        public override IEnumerator OnEnd()
        {
            yield return DestroyAllGameModeObjects();
        }
    }
}