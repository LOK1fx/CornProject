using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LOK1game.Utils
{
    [System.Serializable]
    public class Logger
    {
        public ELoggerGroup Group => _group;

        [SerializeField] private ELoggerGroup _group;
        [SerializeField] private bool _isToggled;
        [SerializeField] private Color _color;

        public Logger(ELoggerGroup group, bool toggle)
        {
            _group = group;
            _isToggled = toggle;
        }

        public Logger(ELoggerGroup group, bool toggle, Color color)
        {
            _group = group;
            _isToggled = toggle;
            _color = color;
        }

        public static void Push(object message, ELoggerGroup group, Object sender)
        {
            App.Loggers.GetLogger(group).Push(message, sender);
        }

        public static void PushWarning(object message, ELoggerGroup group, Object sender)
        {
            App.Loggers.GetLogger(group).PushWarning(message, sender);
        }

        public static void PushError(object message, ELoggerGroup group, Object sender)
        {
            App.Loggers.GetLogger(group).PushError(message, sender);
        }

        public void Push(object message, Object sender = null)
        {
            if (sender != null)
                BasePush(message, sender, Debug.Log);
            else
                BasePush(message, Debug.Log);
        }

        public void PushWarning(object message, Object sender = null)
        {
            if (sender != null)
                BasePush(message, sender, Debug.LogWarning);
            else
                BasePush(message, Debug.LogWarning);
        }

        public void PushError(object message, Object sender = null)
        {
            if (sender != null)
                BasePush(message, sender, Debug.LogError);
            else
                BasePush(message, Debug.LogError);
        }

        private void BasePush(object message, Object sender, Action<object, Object> callback)
        {
            if (_isToggled && sender != null)
                callback?.Invoke(GenerateMessage(message), sender);
        }

        private void BasePush(object message, Action<object> callback)
        {
            if (_isToggled)
                callback?.Invoke(GenerateMessage(message));
        }

        private string GenerateMessage(object message)
        {
            return $"<color={GetHexColor()}>{Group.ToString()}</color>: {message}";
        }

        private string GetHexColor()
        {
            return $"#{ColorUtility.ToHtmlStringRGBA(_color)}";
        }
    }
}
