using UnityEngine;
using System;

namespace LOK1game.Weapon
{
    public abstract class RaycastGun : MonoBehaviour, IWeapon
    {
        public GunData Data => _data;
        public bool CanBeUsed { get; protected set; } = true;

        protected AudioSource Audio { get; private set; }

        [SerializeField] private LayerMask _shootableLayer;
        [SerializeField] private GunData _data;

        private float _timeToNextShoot;

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

        public void Construct(GunData data)
        {
            _data = data;
        }

        public virtual void Use(Player.Player sender)
        {
            if (CanBeUsed == false)
                return;

            var camera = sender.Camera.GetRecoilCameraTransform();

            for (int i = 0; i < _data.ProjectilesPerUseCount; i++)
            {
                Debug.Log(_data.ProjectilesPerUseCount);

                var spread = camera.position + camera.forward * 1000f;

                spread += UnityEngine.Random.Range(-_data.SpreadRadius, _data.SpreadRadius) * camera.up;
                spread += UnityEngine.Random.Range(-_data.SpreadRadius, _data.SpreadRadius) * camera.right;
                spread -= camera.position;
                spread.Normalize();

                if (Physics.Raycast(camera.position, spread, out var hit, _data.ShootDistance, _shootableLayer, QueryTriggerInteraction.Ignore))
                {
                    Debug.DrawRay(hit.point, hit.normal, Color.yellow, 2f);

                    if (hit.collider.gameObject.TryGetComponent<IDamagable>(out var damagable))
                    {
                        var damage = new Damage(_data.Damage, EDamageType.Normal, sender)
                        {
                            HitPoint = hit.point,
                            HitNormal = hit.normal
                        };

                        damagable.TakeDamage(damage);
                    }
                }
            }

            _timeToNextShoot = Time.time + 1f / Data.FireRate;
        }
    }
}