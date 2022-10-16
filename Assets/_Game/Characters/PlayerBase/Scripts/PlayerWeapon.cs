using UnityEngine;
using LOK1game.Player;
using System.Collections.Generic;

namespace LOK1game.Weapon
{
    [RequireComponent(typeof(Player.Player), typeof(PlayerInputProvider))]
    public class PlayerWeapon : MonoBehaviour, IInputabe
    {
        [SerializeField] private List<GunData> _guns;

        private IWeapon _currentWeapon;
        private int _currentWeaponIndex;
        private Player.Player _player;
        private PlayerInputProvider _inputProvider;

        private void Awake()
        {
            _player = GetComponent<Player.Player>();
            _inputProvider = GetComponent<PlayerInputProvider>();
            _inputProvider.Add(this);
        }

        private void Start()
        {
            EquipWeapon(0);
        }

        public void OnInput(object sender)
        {
            if (_player.IsDead)
                return;

            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shoot();
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
        }

        private void Shoot()
        {
            if (_currentWeapon.CanBeUsed)
            {
                _currentWeapon.Use(_player);
                _player.FirstPersonArms.Animator.Play("Shoot", 0, 0f);
            }
        }

        public void EquipWeapon(GunData data)
        {
            if (data.WeaponId == EWeaponId.None)
                throw new System.Exception($"WeaponData {data}' ID is setted to {data.WeaponId}!!");

            _player.FirstPersonArms.ClearRightHand();

            var gun = Instantiate(data.Prefab);

            _player.FirstPersonArms.AttachObjectToRightHand(gun);
            _player.FirstPersonArms.Animator.runtimeAnimatorController = data.AnimatorOverride;
            _player.FirstPersonArms.Animator.Play("Equip", 0, 0f);

            _currentWeapon = gun.GetComponent<IWeapon>();
        }

        public void EquipWeapon(int index)
        {
            _currentWeaponIndex = index;

            EquipWeapon(_guns[index]);
        }
    }
}