using System.Collections;
using UnityEngine;
using Photon.Pun;

namespace LOK1game.Game
{
    public class DefaultNetworkGameMode : BaseGameMode
    {
        public override EGameModeId Id => EGameModeId.NetworkDefault;

        public override IEnumerator OnStart()
        {
            State = EGameModeState.Starting;

            SpawnGameModeObject(CameraPrefab);
            SpawnGameModeObject(UiPrefab);   

            var player = PhotonNetwork.Instantiate(PlayerPrefab.name, GetRandomSpawnPointPosition(), Quaternion.identity);

            CreatePlayerController(player.GetComponent<Pawn>());

            State = EGameModeState.Started;

            yield return null;
        }

        public override IEnumerator OnEnd()
        {
            yield return DestroyAllGameModeObjects();
        }
    }
}