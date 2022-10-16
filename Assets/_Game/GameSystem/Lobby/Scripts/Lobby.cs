using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LOK1game.Tools;
using Photon.Pun;
using UnityEngine.UI;

namespace LOK1game
{
    public class Lobby : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Button _connectButton;

        private void Awake()
        {
            PhotonNetwork.GameVersion = Application.version;
            PhotonNetwork.AutomaticallySyncScene = true;

            PhotonNetwork.ConnectUsingSettings();
        }

        public void Connect()
        {
            PhotonNetwork.JoinRandomOrCreateRoom();
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();

            _connectButton.interactable = true;
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();

            if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
                PhotonNetwork.LoadLevel("SampleScene");
        }
    }
}