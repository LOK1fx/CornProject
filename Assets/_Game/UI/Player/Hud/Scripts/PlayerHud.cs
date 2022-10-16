using UnityEngine;
using TMPro;

namespace LOK1game.UI
{
    public class PlayerHud : MonoBehaviour, IPlayerHud
    {
        [SerializeField] private RectTransform _healthbarBar;
        [SerializeField] private TextMeshProUGUI _healthbarText;
        [SerializeField] private TextMeshProUGUI _actorInfo;
        [SerializeField] private TextMeshProUGUI _weaponNameText;

        private Player.Player _player;
        private PlayerController _playerController;

        public void BindToPlayer(Player.Player player, PlayerController playerController)
        {
            _player = player;
            _playerController = playerController;

            Setup();
        }

        private void Setup()
        {
            _player.OnHealthChanged += OnPlayerHealthChanged;
            _player.Weapon.OnWeaponChanged += OnPlayerWeaponChanged;

            _actorInfo.text = $"Actor:{_player.photonView.OwnerActorNr}";
        }

        private void OnPlayerWeaponChanged(Weapon.GunData data)
        {
            _weaponNameText.text = $"Gun:<b>{data.GunName}</b>";
        }

        private void OnPlayerHealthChanged()
        {
            _healthbarBar.localScale = new Vector3(_player.Health * 0.01f, 1f, 1f);
            _healthbarText.text = _player.Health.ToString();
        }
    }
}