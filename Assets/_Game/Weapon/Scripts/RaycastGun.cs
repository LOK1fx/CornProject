using UnityEngine;
using System;

namespace LOK1game.Weapon
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class RaycastGun : MonoBehaviour, IWeapon
    {
        public GunData Data => _data;
        public bool CanBeUsed { get; protected set; } = true;

        protected AudioSource Audio { get; private set; }

        [SerializeField] private LayerMask _shootableLayer;
        [SerializeField] private GunData _data;

        [Header("Audio")]
        [SerializeField] private AudioClip _shootClip;

        private float _timeToNextShoot;

        private void Awake()
        {
            Audio = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (Time.time > _timeToNextShoot)
            {
                CanBeUsed = true;
            }
            else
            {
                CanBeUsed = false;
            }
        }

        public abstract void AltUse(Player.Player sender);
        public abstract void Equip(Player.Player sender);

        public virtual void Use(Player.Player sender)
        {
            if (CanBeUsed == false)
                return;

            var camera = sender.Camera.GetRecoilCameraTransform();

            if (Physics.Raycast(camera.position, camera.forward, out var hit, _data.ShootDistance, _shootableLayer, QueryTriggerInteraction.Ignore))
            {
                if (hit.collider.gameObject.TryGetComponent<IDamagable>(out var damagable))
                {
                    var damage = new Damage(_data.Damage, EDamageType.Normal, sender);

                    damagable.TakeDamage(damage);
                }
            }

            Audio.PlayOneShot(_shootClip);
            _timeToNextShoot = Time.time + 1f / Data.FireRate;
        }
    }
}