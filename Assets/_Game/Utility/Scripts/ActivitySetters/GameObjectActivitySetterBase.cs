using UnityEngine;
using LOK1game.Game;

namespace LOK1game.Tools
{
    public abstract class GameObjectActivitySetterBase : MonoBehaviour
    {
        [SerializeField] protected bool activateObject;
        [SerializeField] protected GameObject targetGameObject;

        private void Awake()
        {
            App.ProjectContext.GameStateManager.OnGameStateChanged += OnGameStateChanged;
        }

        protected abstract void OnGameStateChanged(EGameState newGameState);

        private void OnDestroy()
        {
            App.ProjectContext.GameStateManager.OnGameStateChanged -= OnGameStateChanged;
        }
    }
}
