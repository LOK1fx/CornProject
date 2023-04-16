using LOK1game.Game;
using System;
using System.Collections.Generic;
using LOK1game.Game.Events;
using UnityEngine;

namespace LOK1game
{
    [Serializable]
    public sealed class ProjectContext : Context
    {
        public GameModeManager GameModeManager => _gameModeManager;
        public GameStateManager GameStateManager { get; private set; }

        public EGameModeId StandardGameModeId => _standardGameModeId;
        
        [Header("GameModes")]
        [SerializeField] private GameModeManager _gameModeManager;
        [SerializeField] private EGameModeId _standardGameModeId;
        [Header("Attention! In list, use only \nobjects with GM_ prefix!")]
        [SerializeField] private List<GameObject> _gameModes = new List<GameObject>();

#if DEVELOPMENT_BUILD || UNITY_EDITOR

        [Space]
        [Header("Dev")]
        [SerializeField] private GameObject _devCardPrefab;

#endif


        public override void Initialize()
        {
            GameStateManager = new GameStateManager();
            _gameModeManager = new GameModeManager();

            if(!PlayerConfig.IsInitialized)
                PlayerConfig.Initialize();

            foreach (var gameModeObject in _gameModes)
            {
                var gameMode = gameModeObject.GetComponent<IGameMode>();
                
                _gameModeManager.AddGameMode(gameMode.Id, gameMode);
            }

            var evt = new OnProjectContextInitializedEvent(this);
            EventManager.Broadcast(evt);

            App.Loggers.GetLogger(ELoggerGroup.BaseInfo).Push("Project context initalized!");

#if UNITY_EDITOR

            if (UnityEditor.EditorUserBuildSettings.development)
            {
                var devCard = GameObject.Instantiate(_devCardPrefab);
                GameObject.DontDestroyOnLoad(devCard);
            }

#elif DEVELOPMENT_BUILD

            var devCard = GameObject.Instantiate(_devCardPrefab);
            GameObject.DontDestroyOnLoad(devCard);

#endif
        }
    }
}
