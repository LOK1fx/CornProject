using UnityEngine;
using TMPro;
using System.Collections;

namespace LOK1game.UI
{
    public class PlayerHud : MonoBehaviour, IPlayerHud
    {
        [SerializeField] private RectTransform _healthbarBar;
        [SerializeField] private TextMeshProUGUI _healthbarText;
        [SerializeField] private TextMeshProUGUI _actorInfo;
        [SerializeField] private TextMeshProUGUI _weaponNameText;
        [SerializeField] private TextMeshProUGUI _weaponAmmoText;
        [SerializeField] private TextMeshProUGUI _respawnCountdownText;
        [SerializeField] private TextMeshProUGUI _reloadingCountdownText;
        [SerializeField] private CanvasGroupFade _damageOverlay;

        [Space]
        [SerializeField] private Animator _hitmarkerAnimator;

        private PlayerDomain.Player _player;
        private PlayerController _playerController;

        public void BindToPlayer(PlayerDomain.Player player, PlayerController playerController)
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
            _player.Weapon.OnStartReloading += OnPlayerStartedReloading;
            _player.Weapon.OnHit += OnPlayerWeaponHit;
            _player.OnDeath += OnPlayerDeath;
            _player.OnTakeDamage += OnPlayerTakeDamage;

            _actorInfo.text = $"Actor:{_player.photonView.OwnerActorNr}";
        }

        private void OnPlayerWeaponHit(Weapon.GunData arg1, Damage arg2)
        {
            _hitmarkerAnimator.Play("Hit", 0, 0);
        }

        private void OnDestroy()
        {
            _player.Health.OnHealthChanged -= OnPlayerHealthChanged;
            _player.Weapon.OnWeaponChanged -= OnPlayerWeaponChanged;
            _player.Weapon.OnAttack -= UpdateAmmoCounter;
            _player.Weapon.OnReloaded -= UpdateAmmoCounter;
            _player.Weapon.OnStartReloading += OnPlayerStartedReloading;
            _player.Weapon.OnHit -= OnPlayerWeaponHit;
            _player.OnDeath -= OnPlayerDeath;
            _player.OnTakeDamage -= OnPlayerTakeDamage;

            StopAllCoroutines();
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
            _healthbarText.text = _player.Health.Hp.ToString();
        }

        private void OnPlayerTakeDamage()
        {
            _damageOverlay.InstaShow();
            _damageOverlay.TargetAlpha = 0f;
        }

        private void OnPlayerStartedReloading(Weapon.GunData gun)
        {
            var routine = CountdownRoutine(Mathf.FloorToInt(gun.ReloadTime), _reloadingCountdownText, "Reloading");
            StartCoroutine(routine);
        }

        private void OnPlayerDeath()
        {
            var routine = CountdownRoutine(Mathf.FloorToInt(_player.RespawnTime), _respawnCountdownText, "To respawning");
            StartCoroutine(routine);
        }

        private IEnumerator CountdownRoutine(int time, TextMeshProUGUI countdownText, string text)
        {
            var remaining = time;

            countdownText.gameObject.SetActive(true);

            for (int i = 0; i < time; i++)
            {
                countdownText.text = $"{text} {remaining}";
                remaining--;
                yield return new WaitForSeconds(1);
            }

            countdownText.gameObject.SetActive(false);
        }
    }
}