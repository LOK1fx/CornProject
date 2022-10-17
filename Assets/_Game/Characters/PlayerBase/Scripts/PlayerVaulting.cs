using System;
using UnityEngine;
using LOK1game.Tools;

namespace LOK1game.Player
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerVaulting : MonoBehaviour
    {
        public event Action<float> OnVault;

        [SerializeField] private LayerMask _groundMask;
        [Range(0, 1)]
        [SerializeField] private float _requiredSpeed = 4f;
        [SerializeField] private Vector3 _speedAfterVaultMultiplier = new Vector3(0.4f, 0f, 0.4f);

        private Player _player;

        private void Start()
        {
            _player = GetComponent<Player>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            var normal = collision.contacts[0].normal;

            if(collision.contacts[0].otherCollider.gameObject.TryGetComponent<Rigidbody>(out var rigidbody))
            {
                if(!rigidbody.isKinematic)
                {
                    return;
                }
            }
            if (IsWall(normal) && _player != null)
            {
                Vault();
            }
        }

        private void Vault()
        {
            var playerMovement = _player.Movement;
            var maxVaultPos = transform.position + Vector3.up * Constants.Gameplay.PLAYER_HEIGHT;
            var dir = playerMovement.ActualMoveDirection;
            dir.y = 0f;
            dir.Normalize();

            if(playerMovement.GetRoundedSpeed() < _requiredSpeed)
            {
                dir = playerMovement.GetDirection(playerMovement.GetInputMoveAxis()).normalized;
            }

            if (Physics.Raycast(maxVaultPos, dir, 1.5f, _groundMask))
            {
                return;
            }

            var hoverPos = maxVaultPos + dir;

            if (!Physics.Raycast(hoverPos, Vector3.down, out RaycastHit hit, 1.5f, _groundMask))
            {
                return;
            }

            var landPos = hit.point;
            var height = landPos.y - transform.position.y;

            if (height > 1f)
                _player.FirstPersonArms.Animator.Play("Climb", 0, 0f);

            _player.Camera.GetCameraTransform().localPosition = transform.position - landPos + _player.Camera.DesiredPosition;

            transform.position = landPos;

            var velocity = playerMovement.Rigidbody.velocity;

            velocity.MultiplyBy(_speedAfterVaultMultiplier);
            playerMovement.Rigidbody.velocity = velocity;
            playerMovement.Land();

            OnVault?.Invoke(height);
        }

        private bool IsWall(Vector3 normal)
        {
            return Math.Abs(90f - Vector3.Angle(Vector3.up, normal)) < 0.1f;
        }
    }
}