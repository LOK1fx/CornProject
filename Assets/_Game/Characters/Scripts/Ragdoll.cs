using UnityEngine;

namespace LOK1game.Character.Generic
{
    public class Ragdoll : MonoBehaviour
    {
        private Rigidbody[] _rigidbodies;
        private Collider[] _colliders;

        private void Awake()
        {
            _rigidbodies = gameObject.GetComponentsInChildren<Rigidbody>();
            _colliders = gameObject.GetComponentsInChildren<Collider>();

            DisableRagdoll();
        }

        public void DisableRagdoll()
        {
            foreach (var rigidbody in _rigidbodies)
            {
                rigidbody.isKinematic = true;
            }
            foreach (var collider in _colliders)
            {
                collider.enabled = false;
            }
        }

        public void EnableRagdoll()
        {
            foreach (var rigidbody in _rigidbodies)
            {
                rigidbody.isKinematic = false;
            }
            foreach (var collider in _colliders)
            {
                collider.enabled = true;
            }
        }

        public void EnableRagdoll(Vector3 force)
        {
            foreach (var rigidbody in _rigidbodies)
            {
                rigidbody.AddForce(force, ForceMode.Impulse);
            }

            EnableRagdoll();
        }
    }
}