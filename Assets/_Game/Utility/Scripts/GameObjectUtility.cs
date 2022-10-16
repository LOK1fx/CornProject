using UnityEngine;

namespace LOK1game.Tools
{
    /// <summary>
    /// Позволяет использовать Instantiate в не MonoBehaviour скрипте
    /// </summary>
    public sealed class GameObjectUtility : MonoBehaviour
    {
        #region Instantiate

        #region Object

        public static Object InstantiateObject(Object original, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            return Instantiate(original, position, rotation, parent);
        }

        public static Object InstantiateObject(Object original, Transform parent, bool stayInWorldSpace = false)
        {
            return Instantiate(original, parent, stayInWorldSpace);
        }

        #endregion

        #region Generic

        public static Object InstantiateObject<T>(T original, Vector3 position, Quaternion rotation, Transform parent = null) where T : Object
        {
            return Instantiate<T>(original, position, rotation, parent);
        }

        public static Object InstantiateObject<T>(T original, Transform parent, bool stayInWorldSpace = false) where T : Object
        {
            return Instantiate<T>(original, parent, stayInWorldSpace);
        }

        #endregion

        #endregion
    }
}