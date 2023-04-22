using LOK1game.Utils;
using LOK1game.World;
using Photon.Pun;

namespace LOK1game
{
    public abstract class Actor : MonoBehaviourPunCallbacks
    {
        public GameWorld CurrentWorld { get; private set; }

        #region Loggers

        public Logger GetBaseInfoLogger()
        {
            return GetLoggers().GetLogger(ELoggerGroup.BaseInfo);
        }

        public Logger GetCurrentWorldLogger()
        {
            return GetLoggers().GetLogger(ELoggerGroup.CurrentWorld);
        }

        public Logger GetPhysicsLogger()
        {
            return GetLoggers().GetLogger(ELoggerGroup.Physics);
        }

        public Logger GetEnvironmentLogger()
        {
            return GetLoggers().GetLogger(ELoggerGroup.Environment);
        }

        public Logger GetSubworldLogger()
        {
            return GetLoggers().GetLogger(ELoggerGroup.Subworld);
        }

        #endregion

        public void PassWorld<T>(T world) where T : GameWorld
        {
            CurrentWorld = world;
        }

        protected virtual void SubscribeToEvents()
        {

        }

        protected virtual void UnsubscribeFromEvents()
        {

        }

        protected ProjectContext GetProjectContext()
        {
            return App.ProjectContext;
        }

        protected Loggers GetLoggers()
        {
            return App.Loggers;
        }
    }
}