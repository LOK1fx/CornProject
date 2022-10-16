using UnityEngine;
using TMPro;

namespace LOK1game
{
    public sealed class PopupText3D : PopupText
    {
        [SerializeField] private TextMeshPro _text;

        private Transform _playerCameraTransform;

        private void Awake()
        {
            _playerCameraTransform = Camera.main.transform;
        }

        private void LateUpdate()
        {
            if (_playerCameraTransform == null) { return; }

            UpdateOffset();
            UpdateAlpha();

            _text.alpha = _alpha;

            transform.localPosition = _initialPosition + _offset;
            transform.LookAt(_playerCameraTransform);
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
