using System;
using UnityEngine;

namespace LOK1game.Character.Generic
{
    public class Ragdoll : Actor
    {
        #region Events

        public event Action OnRagdollActivated;
        public event Action OnRagdollDeactivated;

        #endregion

        private Rigidbody[] _rigidbodies;
        private Collider[] _colliders;

        private float _mediumWeight;

        private void Awake()
        {
            _rigidbodies = gameObject.GetComponentsInChildren<Rigidbody>();
            _colliders = gameObject.GetComponentsInChildren<Collider>();

            DeactivateRagdoll();
        }

        private void Start()
        {
            RecalculateMediumWeight();
        }

        public void DeactivateRagdoll()
        {
            foreach (var rigidbody in _rigidbodies)
            {
                rigidbody.isKinematic = true;
            }
            foreach (var collider in _colliders)
            {
                collider.enabled = false;
            }

            OnRagdollDeactivated?.Invoke();
        }

        public void ActivateRagdoll()
        {
            GetPhysicsLogger().Push($"Ragdoll activated", this);

            foreach (var rigidbody in _rigidbodies)
            {
                rigidbody.isKinematic = false;
            }
            foreach (var collider in _colliders)
            {
                collider.enabled = true;
            }

            OnRagdollActivated?.Invoke();
        }

        public void ActivateRagdoll(Vector3 force)
        {
            ActivateRagdoll();

            foreach (var rigidbody in _rigidbodies)
            {
                rigidbody.AddForce(force, ForceMode.Impulse);

                Debug.DrawRay(rigidbody.position, force * (_mediumWeight * 0.001f), Color.red, 8f);
            }
        }

        private void RecalculateMediumWeight()
        {
            if (_rigidbodies.Length < 1)
                return;

            var summedWeight = 0f;

            foreach (var rigidbody in _rigidbodies)
            {
                summedWeight += rigidbody.mass;
            }

            _mediumWeight = summedWeight / _rigidbodies.Length;
        }
    }
}