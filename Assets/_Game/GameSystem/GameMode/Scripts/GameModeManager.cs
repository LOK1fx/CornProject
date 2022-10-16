using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LOK1game.Tools;

namespace LOK1game.Game
{
    public struct GameModeContainer
    {
        public EGameModeId Id;
        public IGameMode GameMode;

        public GameModeContainer(EGameModeId id, IGameMode gameMode)
        {
            Id = id;
            GameMode = gameMode;
        }
    }

    public sealed class GameModeManager
    {
        public IGameMode CurrentGameMode { get; private set; }
        public EGameModeId CurrentGameModeId
        {
            get
            {
                return CurrentGameMode.Id;
            }
        }

        private readonly List<GameModeContainer> _gameModes = new List<GameModeContainer>();
        private bool _isSwithing;

        public void AddGameMode(EGameModeId id, IGameMode mode)
        {
            _gameModes.Add(new GameModeContainer(id, mode));
        }

        public void SetGameMode(EGameModeId id)
        {
            var gamemode = GetGameMode(id);

            Coroutines.StartRoutine(SwitchGameModeRoutine(gamemode));
        }

        private IEnumerator SwitchGameModeRoutine(IGameMode gameMode)
        {
            yield return new WaitUntil(() => !_isSwithing);

            if (CurrentGameMode == gameMode)
            {
                yield break;
            }

            _isSwithing = true;

            if (CurrentGameMode != null)
            {
                yield return CurrentGameMode.OnEnd();
            }

            CurrentGameMode = gameMode;

            yield return CurrentGameMode.OnStart();

            _isSwithing = false;
        }

        private IGameMode GetGameMode(EGameModeId id)
        {
            foreach (var gamemode in _gameModes)
            {
                if (gamemode.Id == id)
                {
                    return gamemode.GameMode;
                }
            }

            throw new System.ArgumentException();
        }
    }
}