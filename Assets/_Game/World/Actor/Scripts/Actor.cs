using LOK1game.Utils;
using Photon.Pun;

namespace LOK1game
{
    public abstract class Actor : MonoBehaviourPunCallbacks
    {
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