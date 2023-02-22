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
        [SerializeField] private TextMeshProUGUI _weaponAmmoText;

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
            _player.Health.OnHealthChanged += OnPlayerHealthChanged;
            _player.Weapon.OnWeaponChanged += OnPlayerWeaponChanged;
            _player.Weapon.OnAttack += UpdateAmmoCounter;
            _player.Weapon.OnReloaded += UpdateAmmoCounter;

            _actorInfo.text = $"Actor:{_player.photonView.OwnerActorNr}";
        }

        private void OnDestroy()
        {
            _player.Health.OnHealthChanged -= OnPlayerHealthChanged;
            _player.Weapon.OnWeaponChanged -= OnPlayerWeaponChanged;
            _player.Weapon.OnAttack -= UpdateAmmoCounter;
            _player.Weapon.OnReloaded -= UpdateAmmoCounter;
        }

        private void OnPlayerWeaponChanged(Weapon.GunData data)
        {
            _weaponNameText.text = $"Gun:<b>{data.GunName}</b>";

            UpdateAmmoCounter(data);
        }

        private void UpdateAmmoCounter(Weapon.GunData data)
        {
            _weaponAmmoText.text = $"{data.Clip}|{data.Stash}";
        }

        private void OnPlayerHealthChanged(int hp)
        {
            _healthbarBar.localScale = new Vector3(hp * 0.01f, 1f, 1f);
            _healthbarText.text = _player.Health.ToString();
        }
    }
}