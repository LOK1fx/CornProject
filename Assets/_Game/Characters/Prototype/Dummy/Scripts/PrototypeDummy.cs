using LOK1game.Character.Generic;
using System.Collections;
using UnityEngine;

namespace LOK1game.Character.Prototype
{
    [RequireComponent(typeof(Collider), typeof(Health))]
    public class PrototypeDummy : Actor, IDamagable
    {
        [SerializeField] private float _respawnTime = 3f;
        [SerializeField] private Ragdoll _ragdoll;

        private Vector3 _startSize;
        private bool _isDead;

        private CapsuleCollider _collider;
        private Health _health;


        private void Awake()
        {
            _collider = GetComponent<CapsuleCollider>();
            _health = GetComponent<Health>();

            _startSize = transform.localScale;
        }

        private void LateUpdate()
        {
            transform.localScale = Vector3.Lerp(transform.localScale, _startSize, Time.deltaTime * 7f);
        }

        public void TakeDamage(Damage damage)
        {
            if (_isDead)
                return;

            transform.localScale *= 1.1f;

            _health.ReduceHealth(damage.Value);

            if (_health.Hp <= 0)
                Death(damage.GetHitDirection() * damage.Value * 0.5f);
        }

        private void Death(Vector3 force)
        {
            _isDead = true;
            _collider.enabled = false;

            _ragdoll.ActivateRagdoll(force);

            StartCoroutine(RespawnRoutine());
        }

        private IEnumerator RespawnRoutine()
        {
            yield return new WaitForSeconds(_respawnTime);

            _isDead = false;
            _collider.enabled = true;

            _health.ResetHealth();

            _ragdoll.DeactivateRagdoll();
        }
    }
}