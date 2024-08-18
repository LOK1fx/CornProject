using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace LOK1game.PlayerDomain
{
    [RequireComponent(typeof(Player))]
    public class PlayerInputProvider : MonoBehaviourPunCallbacks
    {
        [SerializeField] private List<IInputabe> _inputables = new List<IInputabe>();

        private Player _player;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        public void Add(IInputabe inputabe)
        {
            _inputables.Add(inputabe);
        }

        private void Update()
        {
            if (photonView.IsMine == false)
                return;

            foreach (var inputable in _inputables)
            {
                inputable.OnInput(_player);
            }
        }
    }
}