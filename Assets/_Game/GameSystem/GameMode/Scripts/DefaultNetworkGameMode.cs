using System.Collections;
using UnityEngine;
using Photon.Pun;
using LOK1game.UI;

namespace LOK1game.Game
{
    public class DefaultNetworkGameMode : BaseGameMode
    {
        public override EGameModeId Id => EGameModeId.NetworkDefault;

        public override IEnumerator OnStart()
        {
            State = EGameModeState.Starting;

            SpawnGameModeObject(CameraPrefab);

            var ui = SpawnGameModeObject(UiPrefab);   
            var player = PhotonNetwork.Instantiate(PlayerPrefab.name, GetRandomSpawnPointPosition(), Quaternion.identity);
            var playerController = CreatePlayerController(player.GetComponent<Pawn>());

            if (ui.TryGetComponent<IPlayerHud>(out var playerHud))
            {
                playerHud.BindToPlayer(player.GetComponent<PlayerDomain.Player>(), playerController);
            }

            State = EGameModeState.Started;

            yield return null;
        }

        public override IEnumerator OnEnd()
        {
            yield return DestroyAllGameModeObjects();
        }
    }
}