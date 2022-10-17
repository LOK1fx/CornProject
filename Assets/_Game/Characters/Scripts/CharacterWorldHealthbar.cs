using UnityEngine;

namespace LOK1game.Character.Generic
{
    public class CharacterWorldHealthbar : MonoBehaviour
    {
        [SerializeField] private RectTransform _barTransform;
        [SerializeField] private Player.Player _character; //Player type needed to Replace with Health class

        private void Awake()
        {
            _character.OnHealthChanged += OnCharacterHealthChanged;
        }

        private void OnDestroy()
        {
            _character.OnHealthChanged -= OnCharacterHealthChanged;
        }

        private void OnCharacterHealthChanged()
        {
            _barTransform.localScale = new Vector3(_character.Health * 0.01f, 1f, 1f);
        }
    }
}