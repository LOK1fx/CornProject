using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LOK1game.Tools;

namespace LOK1game.Game
{
    public class TestGameMode : MonoBehaviour, IGameMode
    {
        public EGameModeId Id => EGameModeId.Test;

        public EGameModeState State { get; set; }

        [SerializeField] private GameObject _maniken;

        public IEnumerator OnStart()
        {
            var m = Instantiate(_maniken);

            yield return null;
        }

        public IEnumerator OnEnd()
        {
            yield return null;
        }
    }
}