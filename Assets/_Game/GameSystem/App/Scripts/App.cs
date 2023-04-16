using System;
using System.Collections.Generic;
using LOK1game.Game.Events;
using LOK1game.Utils;
using UnityEngine;
using Logger = LOK1game.Utils.Logger;

namespace LOK1game
{
    public sealed class App : MonoBehaviour
    {
        public static ProjectContext ProjectContext { get; private set; }
        public static Loggers Loggers { get; private set; }

        [SerializeField] private ProjectContext _projectContext = new ProjectContext();

        private const string APP_GAME_OBJECT_NAME = "[App]";

        #region Boot

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Bootstrap()
        {
            var app = Instantiate(Resources.Load<App>(APP_GAME_OBJECT_NAME));

            if (app == null)
            {
                throw new ApplicationException();
            }

            app.name = APP_GAME_OBJECT_NAME;
            app.InitializeComponents();

            DontDestroyOnLoad(app.gameObject);
        }

        #endregion

        public static void Quit(int exitCode = 0)
        {
            EventManager.Clear();
            Debug.LogWarning("The EventManager has been cleared!");

            Application.Quit(exitCode);
            Debug.Log("Application quit!");

#if UNITY_EDITOR

            UnityEditor.EditorApplication.isPlaying = false;
            
#endif
        }

        private void InitializeComponents()
        {
            if (Application.isMobilePlatform)
                Application.targetFrameRate = Screen.currentResolution.refreshRate;

            InitializeLoggers();

            ProjectContext = _projectContext;
            ProjectContext.Initialize();
        }

        private void InitializeLoggers()
        {
            var loggers = new Dictionary<ELoggerGroup, Logger>();

            CreateLogger(ELoggerGroup.Application, Color.yellow, ref loggers);
            CreateLogger(ELoggerGroup.BaseInfo, Color.white, ref loggers);
            CreateLogger(ELoggerGroup.Environment, Color.grey, ref loggers);
            CreateLogger(ELoggerGroup.CurrentWorld, Color.green, ref loggers);
            CreateLogger(ELoggerGroup.Networking, Color.green, ref loggers);
            CreateLogger(ELoggerGroup.Player, Color.blue, ref loggers);
            CreateLogger(ELoggerGroup.Physics, Color.yellow, ref loggers);

            Loggers = new Loggers(loggers);
        }

        private void CreateLogger(ELoggerGroup group, Color color, ref Dictionary<ELoggerGroup, Logger> loggers)
        {
            var logger = new Logger(group, true, color);

            loggers.Add(group, logger);
        }
    }
}