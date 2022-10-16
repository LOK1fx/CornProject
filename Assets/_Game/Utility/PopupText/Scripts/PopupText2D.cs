using UnityEngine;
using TMPro;

namespace LOK1game
{
    public sealed class PopupText2D : PopupText
    {
        [SerializeField] private TextMeshProUGUI _text;

        private void LateUpdate()
        {
            UpdateOffset();
            UpdateAlpha();

            _text.alpha = _alpha;

            transform.position = _initialPosition + _offset;
        }

        public override void Show(PopupTextParams textParams)
        {
            _params = textParams;

            _text.text = _params.Text;
            _text.color = _params.TextColor;
            _disappearTimer = _params.DisappearTime;
        }
    }
}