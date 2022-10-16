using Photon.Pun;

namespace LOK1game
{
    public abstract class Actor : MonoBehaviourPunCallbacks
    {
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
    }
}