using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LOK1game.Tools;
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