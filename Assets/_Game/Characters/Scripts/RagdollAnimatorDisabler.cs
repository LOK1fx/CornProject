using UnityEngine;

namespace LOK1game.Character.Generic
{
    [RequireComponent(typeof(Animator), typeof(Ragdoll))]
    public class RagdollAnimatorDisabler : MonoBehaviour
    {
        private Animator _animator;
        private Ragdoll _ragdoll;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _ragdoll = GetComponent<Ragdoll>();
        }

        private void Start()
        {
            _ragdoll.OnRagdollActivated += OnRagdollActivated;
            _ragdoll.OnRagdollDeactivated += OnRagdollDeactivated;
        }

        private void OnDestroy()
        {
            _ragdoll.OnRagdollActivated -= OnRagdollActivated;
            _ragdoll.OnRagdollDeactivated -= OnRagdollDeactivated;
        }

        private void OnRagdollDeactivated()
        {
            _animator.enabled = true;
        }

        private void OnRagdollActivated()
        {
            _animator.enabled = false;
        }
    }
}