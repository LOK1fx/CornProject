using UnityEngine;
using Photon.Pun;

namespace LOK1game.World
{
    public class NetworkGameWorld : GameWorld
    {
        protected override void Initialize()
        {
            if (PhotonNetwork.IsConnected == false)
            {
                PhotonNetwork.OfflineMode = true;
                PhotonNetwork.JoinRandomOrCreateRoom();

                Debug.LogWarning("There are no connection to Server! You are playing offline.");
            }
        }
    }
}