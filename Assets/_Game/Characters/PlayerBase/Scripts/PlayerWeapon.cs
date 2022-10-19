using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

namespace LOK1game.Weapon
{
    public class PlayerWeapon : MonoBehaviour, IInputabe
    {
        public event Action<GunData> OnWeaponChanged;
        public event Action<GunData> OnAttack;
        public event Action<GunData> OnReloaded;

        public bool IsReloading { get; private set; }

        [SerializeField] private List<GunData> _loadout;
        [SerializeField] private Transform _weaponHolder;

        private IWeapon _currentWeapon;
        private int _currentWeaponIndex;
        private Player.Player _player;

        public void Construct(Player.Player player)
        {
            _player = player;
        }

        private void Start()
        {
            InitializeLoadoutAmmo();
            EquipWeapon(0);

            _player.OnRespawned += OnPlayerRespawned;
            _player.OnDeath += OnPlayerDeath;
        }

        private void OnDestroy()
        {
            _player.OnRespawned -= OnPlayerRespawned;
            _player.OnDeath -= OnPlayerDeath;
        }

        public void OnInput(object sender)
        {
            if (_player.IsDead)
                return;

            if(_loadout[_currentWeaponIndex].Auto)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    Attack();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Attack();
                }
            }

            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                EquipWeapon(0);
            }
            if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                EquipWeapon(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                EquipWeapon(2);
            }

            if(Input.GetKeyDown(KeyCode.F))
            {
                _player.FirstPersonArms.Animator.Play("Inspect", 0, 0f);
            }
            if(Input.GetKeyDown(KeyCode.R))
            {
                if(IsReloading == false)
                    StartCoroutine(ReloadRoutine());
            }
        }

        public void EquipWeapon(GunData data)
        {
            if (data.WeaponId == EWeaponId.None)
                throw new Exception($"WeaponData {data}' ID is setted to {data.WeaponId}!!");

            _player.FirstPersonArms.ClearRightHand();

            var gun = Instantiate(data.Prefab);

            _player.FirstPersonArms.AttachObjectToRightHand(gun);
            _player.FirstPersonArms.Animator.runtimeAnimatorController = data.AnimatorOverride;
            _player.FirstPersonArms.Animator.Play("Equip", 0, 0f);

            _currentWeapon = gun.GetComponent<IWeapon>();

            OnWeaponChanged?.Invoke(data);
        }

        public void EquipWeapon(int index)
        {
            _currentWeaponIndex = index;

            EquipWeapon(_loadout[index]);
        }

        private IEnumerator ReloadRoutine()
        {
            var weapon = _loadout[_currentWeaponIndex];
            IsReloading = true;

            yield return new WaitForSeconds(_loadout[_currentWeaponIndex].ReloadTime);

            weapon.Reload();

            IsReloading = false;
            OnReloaded?.Invoke(weapon);
        }

        private void Attack()
        {
            if (_loadout[_currentWeaponIndex].Clip == 0)
                StartCoroutine(ReloadRoutine());

            if (_currentWeapon.CanBeUsed && _loadout[_currentWeaponIndex].TryFireBullet() && IsReloading == false)
            {
                _currentWeapon.Use(_player);
                _player.FirstPersonArms.Animator.Play("Shoot", 0, 0f);

                OnAttack?.Invoke(_loadout[_currentWeaponIndex]);
            }
        }

        private void OnPlayerRespawned()
        {
            InitializeLoadoutAmmo();

            _weaponHolder.gameObject.SetActive(true);
            EquipWeapon(0);
        }

        private void OnPlayerDeath()
        {
            _weaponHolder.gameObject.SetActive(false);
        }

        private void InitializeLoadoutAmmo()
        {
            foreach (var weapon in _loadout)
            {
                weapon.InitializeAmmo();
            }
        }
    }
}