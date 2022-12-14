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
        public float ReloadTime => _reloadTime;


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
        [SerializeField] private float _reloadTime = 1f;

        [HideInInspector] public int Stash { get; private set; }
        [HideInInspector] public int Clip { get; private set; }

        public void InitializeAmmo()
        {
            Stash = _ammo;
            Clip = _clipAmmo;
        }

        public void Reload()
        {
            Stash += Clip;
            Clip = Mathf.Min(_clipAmmo, Stash);
            Stash -= Clip;
        }

        public bool TryFireBullet()
        {
            if (Clip > 0)
            {
                Clip -= 1;
                return true;
            }
            
            return false;
        }
    }
}