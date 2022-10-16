using System;
using UnityEngine;

namespace LOK1game.Tools
{
    /// <summary>
    /// Позволяет использовать Update и FixedUpdate в не MonoBehaviour скрипте
    /// </summary>
    public class CustomUpdate : MonoBehaviour
    {
        private static CustomUpdate _instance
        {
            get
            {
                if (m_instance != null) return m_instance;
                
                var go = new GameObject("[Custom Update]");
                m_instance = go.AddComponent<CustomUpdate>();
                DontDestroyOnLoad(go);

                return m_instance;
            }
        }

        private static CustomUpdate m_instance;
        
        private Action _updateCallback;
        private Action _fixedUpdateCallback;
        
        private void Update()
        {
            _updateCallback?.Invoke();
        }

        private void FixedUpdate()
        {
            _fixedUpdateCallback?.Invoke();
        }
        
        public static void AddUpdateCallback(Action callback)
        {
            _instance._updateCallback += callback;
        }

        public static void RemoveUpdateCallback(Action callback)
        {
            _instance._updateCallback -= callback;
        }

        public static void AddFixedUpdateCallback(Action callback)
        {
            _instance._fixedUpdateCallback += callback;
        }

        public static void RemoveFixedUpdateCallback(Action callback)
        {
            _instance._fixedUpdateCallback -= callback;
        }
    }
}