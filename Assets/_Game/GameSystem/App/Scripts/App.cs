using System;
using System.Collections.Generic;
using LOK1game.Game.Events;
using LOK1game.Utils;
using UnityEngine;

namespace LOK1game
{
    public sealed class App : MonoBehaviour
    {
        public static ProjectContext ProjectContext { get; private set; }
        public static Loggers Loggers { get; private set; }

        [SerializeField] private ProjectContext _projectContext = new ProjectContext();
        [SerializeField] private List<LoggerContainer> _loggerContainers = new List<LoggerContainer>();

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
            Loggers = new Loggers(_loggerContainers.ToArray());
        }

        [ContextMenu("Swap loggers")]
        private void SwapLoggers()
        {
            if (Loggers != null)
                Loggers.SwapLoggers(_loggerContainers.ToArray());
        }
    }
}