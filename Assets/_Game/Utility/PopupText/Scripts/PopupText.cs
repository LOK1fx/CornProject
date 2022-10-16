using UnityEngine;

namespace LOK1game
{
    public abstract class PopupText : MonoBehaviour
    {
        private const string PATH = "PopupText";

        public float DisappearSpeed = 3f;
        public Vector3 TextMoveDirection;
        public float TextMoveSpeed = 15f;

        protected PopupTextParams _params;
        protected Vector3 _offset;
        protected float _alpha = 1;

        protected float _disappearTimer;
        protected Vector3 _initialPosition;

        public static PopupText Spawn<T>(Vector3 position, Transform parent, PopupTextParams textParams) where T : PopupText
        {
            return BaseSpawn<T>(position, parent, textParams);
        }

        public static PopupText Spawn<T>(Vector3 position, PopupTextParams textParams) where T : PopupText
        {
            return BaseSpawn<T>(position, null, textParams);
        }

        private static PopupText BaseSpawn<T>(Vector3 position, Transform parent, PopupTextParams textParams) where T : PopupText
        {
            var go = Instantiate(Resources.Load<T>(PATH), position, Quaternion.identity, parent);

            go.Show(textParams);

            return go;
        }

        public abstract void Show(PopupTextParams textParams);

        public void SetPosition(Vector3 position)
        {
            _initialPosition = position;
        }

        protected void UpdateOffset()
        {
            _offset += (TextMoveDirection.normalized * TextMoveSpeed) * Time.deltaTime;
        }

        protected void UpdateAlpha()
        {
            _disappearTimer -= Time.deltaTime;
            if (_disappearTimer < 0)
            {
                _alpha -= DisappearSpeed * Time.deltaTime;

                if (_alpha < 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    public struct PopupTextParams
    {
        public string Text { get; private set; }
        public Color TextColor { get; private set; }
        public float DisappearTime { get; private set; }

        public PopupTextParams(string text, float disappearTime)
        {
            Text = text;
            TextColor = Color.white;
            DisappearTime = disappearTime;
        }

        public PopupTextParams(string text, Color color)
        {
            Text = text;
            TextColor = color;
            DisappearTime = 2f;
        }

        public PopupTextParams(string text, float disappearTime, Color color)
        {
            Text = text;
            TextColor = color;
            DisappearTime = disappearTime;
        }
    }
}