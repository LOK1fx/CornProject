using System.Collections;
using UnityEngine;
using Photon.Pun;

namespace LOK1game.Game
{
    public class LobbyGameMode : MonoBehaviour, IGameMode
    {
        public EGameModeId Id => EGameModeId.Lobby;
        public EGameModeState State { get; private set; }

        public IEnumerator OnStart()
        {
            State = EGameModeState.Starting;

            yield return null;
        }


        public IEnumerator OnEnd()
        {
            yield return null;
        }
    }
}