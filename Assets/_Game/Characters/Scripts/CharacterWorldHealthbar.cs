using UnityEngine;

namespace LOK1game.Character.Generic
{
    public class CharacterWorldHealthbar : MonoBehaviour
    {
        [SerializeField] private RectTransform _barTransform;
        [SerializeField] private Health _health; //Player type needed to Replace with Health class

        private void Awake()
        {
            _health.OnHealthChanged += OnHealthChanged;
        }

        private void OnDestroy()
        {
            _health.OnHealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(int hp)
        {
            _barTransform.localScale = new Vector3(hp * 0.01f, 1f, 1f);
        }
    }
}