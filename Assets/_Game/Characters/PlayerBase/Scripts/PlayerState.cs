using UnityEngine;

namespace LOK1game.Player
{
    public class PlayerState : MonoBehaviour
    {
        public bool OnGround { get; private set; }
        public bool IsSliding { get; private set; }
        public bool IsSprinting { get; private set; }
        public bool IsCrouching { get; private set; }
        public bool InTransport { get; private set; }
        public bool IsWallruning { get; private set; }

        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private Vector3 _groundCheckerPosition;
        [SerializeField] private Vector3 _groundCheckerSize;

        private PlayerMovement _playerMovement;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();

            _playerMovement.OnStartCrouch += () => IsCrouching = true;
            _playerMovement.OnStartSlide += () => IsSliding = true;
            _playerMovement.OnStopCrouch += OnStopCrouching;
        }

        private void FixedUpdate()
        {
            var center = transform.position + _groundCheckerPosition;

            if (Physics.CheckBox(center, _groundCheckerSize, Quaternion.identity, _groundMask, QueryTriggerInteraction.Ignore))
            {
                OnGround = true;
            }
            else
            {
                OnGround = false;
            }
        }

        private void OnStopCrouching()
        {
            IsCrouching = false;
            IsSliding = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position + _groundCheckerPosition, _groundCheckerSize * 2);
        }
    }
}