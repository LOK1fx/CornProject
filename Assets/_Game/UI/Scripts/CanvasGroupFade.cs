using System;
using UnityEngine;

namespace LOK1game.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasGroupFade : MonoBehaviour
    {
        public float TargetAlpha;

        [SerializeField] [Range(0f, 1f)] private float _startAlpha;
        [SerializeField] private float _fadeSpeed;

        private CanvasGroup _canvas;

        private void Awake()
        {
            _canvas = GetComponent<CanvasGroup>();
            _canvas.alpha = _startAlpha;
            TargetAlpha = _startAlpha;
        }

        private void Update()
        {
            _canvas.alpha = Mathf.Lerp(_canvas.alpha, TargetAlpha, _fadeSpeed * Time.deltaTime);
        }

        public void SetAlpha(float alpha)
        {
            alpha = Mathf.Clamp01(alpha);

            if (alpha == 0)
            {
                Hide();
            }
            else if (alpha == 1)
            {
                Show();
            }

            TargetAlpha = alpha;
        }

        public void Show()
        {
            TargetAlpha = 1f;

            _canvas.interactable = true;
            _canvas.blocksRaycasts = true;
        }

        public void InstaShow()
        {
            TargetAlpha = 1f;
            _canvas.alpha = 1f;

            _canvas.interactable = true;
            _canvas.blocksRaycasts = true;
        }

        public void Hide()
        {
            TargetAlpha = 0f;

            _canvas.interactable = false;
            _canvas.blocksRaycasts = false;
        }

        public void InstaHide()
        {
            TargetAlpha = 0f;
            _canvas.alpha = 0f;

            _canvas.interactable = false;
            _canvas.blocksRaycasts = false;
        }
    }
}