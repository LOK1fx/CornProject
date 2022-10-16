using System.Collections;

namespace LOK1game.Game
{
    public interface IGameMode
    {
        EGameModeId Id { get; }
        EGameModeState State { get; }
        IEnumerator OnStart();
        IEnumerator OnEnd();
    }
}