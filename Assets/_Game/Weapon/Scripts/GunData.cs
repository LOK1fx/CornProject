using UnityEngine;

namespace LOK1game.Weapon
{
    [CreateAssetMenu(fileName = "new Gun", menuName = "Weapon/Gun")]
    public class GunData : ScriptableObject
    {
        public EWeaponId WeaponId => _weaponId;

        public GameObject Prefab => _prefab;
        public AnimatorOverrideController AnimatorOverride => _animatorOverride;

        public string GunName => _gunName;
        public int ClipAmmo => _clipAmmo;
        public int Ammo => _ammo;
        public bool Auto => _auto;
        public int Damage => _damage;
        public float ShootDistance => _shootDistance;
        public Vector3 Recoil => _recoil;
        public float FireRate => _fireRate;


        [SerializeField] private EWeaponId _weaponId = EWeaponId.None;

        [SerializeField] private GameObject _prefab;
        [SerializeField] private AnimatorOverrideController _animatorOverride;

        [Space]
        [SerializeField] private string _gunName = "Gun";
        [SerializeField] private int _clipAmmo = 8;
        [SerializeField] private int _ammo = 120;
        [SerializeField] private bool _auto;
        [SerializeField] private int _damage = 10;
        [SerializeField] private float _shootDistance = 100f;
        [SerializeField] private Vector3 _recoil = Vector3.right;
        [SerializeField] private float _fireRate = 0.25f;
    }
}