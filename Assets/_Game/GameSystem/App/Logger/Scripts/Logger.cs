using UnityEngine;

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

        public void Push(object message, Object sender = null)
        {
            if (_isToggled && sender != null)
                Debug.Log(GenerateMessage(message), sender);
            else if (_isToggled)
                Debug.Log(GenerateMessage(message));
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
