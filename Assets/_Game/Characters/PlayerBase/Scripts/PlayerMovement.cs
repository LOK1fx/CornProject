using System;
using UnityEngine;
using Photon.Pun;

namespace LOK1game.Player
{
    [RequireComponent(typeof(PlayerState), typeof(Rigidbody), typeof(CapsuleCollider))]
    public class PlayerMovement : MonoBehaviourPunCallbacks
    {
        #region Events

        public event Action OnLand;
        public event Action OnJump;
        public event Action OnStartCrouch;
        public event Action OnStopCrouch;
        public event Action OnStartSlide;
        public event Action OnMovementProcessed;

        #endregion

        public Vector3 ActualMoveDirection { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        public CapsuleCollider PlayerCollider { get; private set; }

        [SerializeField] private LayerMask _groundMask;

        [SerializeField] private float _crouchPlayerHeight = 1.5f;
        private float _defaultPlayerHeight;

        [SerializeField] private float _jumpCooldown = 0.1f;
        private float _currentJumpCooldown;

        [SerializeField] private float _maxSlideTime;
        private float _currentSlideTime;

        public Transform DirectionTransform => _directionTransform;

        [SerializeField] private Transform _directionTransform;
        [SerializeField] private PlayerMovementParams _movementData;

        private PlayerState _playerState;

        private Vector3 _moveDirection;
        private RaycastHit _slopeHit;
        private Vector2 _iAxis = new Vector2(0f, 0f);

        private Vector3 _oldPosition;

        private void Awake()
        {
            _currentJumpCooldown = _jumpCooldown;

            Rigidbody = GetComponent<Rigidbody>();
            PlayerCollider = GetComponent<CapsuleCollider>();
            _playerState = GetComponent<PlayerState>();

            _defaultPlayerHeight = PlayerCollider.height;

            _oldPosition = transform.position;
        }

        private void Update()
        {
            UpdateCooldowns();

            if (_playerState.IsSliding && _playerState.OnGround)
            {
                _currentSlideTime += Time.deltaTime;
            }
            if (_currentSlideTime >= _maxSlideTime)
            {
                Rigidbody.velocity = Vector3.zero;

                StopCrouch();
                StartCrouch();

                _currentSlideTime = 0f;
            }

            ActualMoveDirection = (transform.position - _oldPosition).normalized;

            _oldPosition = transform.position;
        }

        private void FixedUpdate()
        {
            Move();
        }

        public void SetAxisInput(Vector2 input)
        {
            _iAxis = input;
        }

        private void Move()
        {
            if (!_playerState.InTransport)
            {
                if (_playerState.OnGround && Rigidbody.velocity.y <= -4f)
                {
                    Land();
                }

                Vector3 velocity;

                var moveParams = new CharacterMath.MoveParams(GetNonNormDirection(_iAxis), Rigidbody.velocity);
                var slopeMoveParams = new CharacterMath.MoveParams(GetSlopeDirection(_iAxis), Rigidbody.velocity);
                var slideMoveParams = new CharacterMath.MoveParams(Vector3.zero, Rigidbody.velocity);

                if (_playerState.OnGround && !OnSlope() && !_playerState.IsSliding)
                {
                    velocity = MoveGround(moveParams, _playerState.IsSprinting, _playerState.IsCrouching);
                }
                else if (_playerState.OnGround && OnSlope() && !_playerState.IsSliding)
                {
                    velocity = MoveGround(slopeMoveParams, _playerState.IsSprinting, _playerState.IsCrouching);
                    Rigidbody.AddForce(GetSlopeDirection(_iAxis).normalized * 8f, ForceMode.Acceleration);
                }
                else if (_playerState.IsSliding)
                {
                    velocity = MoveAir(slideMoveParams);
                }
                else
                {
                    velocity = MoveAir(moveParams);
                }

                Rigidbody.velocity = velocity;
            }

            OnMovementProcessed?.Invoke();
        }

        public void Jump()
        {
            if (CanJump() && _playerState.OnGround)
            {
                var velocity = new Vector3(Rigidbody.velocity.x, 0f, Rigidbody.velocity.z);

                Rigidbody.velocity = velocity;
                Rigidbody.AddForce(transform.up * _movementData.JumpForce, ForceMode.Impulse);

                ResetJumpCooldown();

                OnJump?.Invoke();
            }
        }

        public void StartCrouch()
        {
            if (_playerState.IsWallruning) { return; }

            _currentSlideTime = 0f;

            PlayerCollider.center = Vector3.up * 0.75f;
            PlayerCollider.height = _crouchPlayerHeight;

            OnStartCrouch?.Invoke();
        }

        public void StartSlide()
        {
            if(!_playerState.OnGround) { return; }

            _currentSlideTime = 0f;

            var dir = GetDirection(_iAxis);

            Rigidbody.AddForce(dir * 40f, ForceMode.Impulse);

            OnStartSlide?.Invoke();
        }

        public void StopCrouch()
        {
            PlayerCollider.height = _defaultPlayerHeight;
            PlayerCollider.center = Vector3.up;

            OnStopCrouch?.Invoke();
        }

        public bool CanStand()
        {
            if (Physics.Raycast(transform.position, Vector3.up, out RaycastHit hit, 2f, _groundMask, QueryTriggerInteraction.Ignore))
                return false;

            return true;
        }

        public bool CanJump()
        {
            if (_currentJumpCooldown <= 0 && !_playerState.InTransport)
            {
                return true;
            }

            return false;
        }

        public void Land()
        {
            var velocity = new Vector3(Rigidbody.velocity.x, 0f, Rigidbody.velocity.z);

            Rigidbody.velocity = velocity;

            OnLand?.Invoke();
        }

        private void UpdateCooldowns()
        {
            if (_currentJumpCooldown > 0)
                _currentJumpCooldown -= Time.deltaTime;
        }

        public void ResetJumpCooldown()
        {
            _currentJumpCooldown = _jumpCooldown;
        }

        private bool OnSlope()
        {
            if (Physics.Raycast(transform.position + (Vector3.up * 0.2f), -transform.up, out _slopeHit, 0.3f, _groundMask))
            {
                if (_slopeHit.normal != Vector3.up)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public Vector3 GetSlopeDirection(Vector2 input)
        {
            return CharacterMath.Project(GetDirection(input), _slopeHit.normal);
        }

        /// <summary>
        /// Возращает не нормализованое напровление ввода относительно игрока.
        /// Подходит для управления с геймпада
        /// </summary>
        /// <param name="input">Ввод</param>
        /// <returns>Direction</returns>
        public Vector3 GetNonNormDirection(Vector2 input)
        {
            var direction = new Vector3(input.x, 0f, input.y);

            direction = DirectionTransform.TransformDirection(direction);

            _moveDirection = direction;

            return direction;
        }

        public Vector3 GetDirection(Vector2 input)
        {
            return GetNonNormDirection(input).normalized;
        }

        public Vector2 GetInputMoveAxis()
        {
            return _iAxis;
        }

        public int GetRoundedSpeed()
        {
            return Mathf.RoundToInt(GetSpeed());
        }

        public float GetSpeed()
        {
            return new Vector3(Rigidbody.velocity.x, 0f, Rigidbody.velocity.z).magnitude;
        }

        #region Acceleratation

        public Vector3 MoveGround(CharacterMath.MoveParams moveParams, bool sprint = false, bool crouch = false)
        {
            float t_speed = moveParams.PreviousVelocity.magnitude;

            if (t_speed != 0)
            {
                float drop = t_speed * _movementData.Friction * Time.fixedDeltaTime;
                moveParams.PreviousVelocity *= Mathf.Max(t_speed - drop, 0) / t_speed;
            }

            if (!sprint && !crouch)
            {
                return AccelerateVelocity(_movementData.WalkGroundAccelerate, _movementData.WalkGroundMaxVelocity, moveParams);
            }
            else if (!crouch)
            {
                return AccelerateVelocity(_movementData.SprintGoundAccelerate, _movementData.SprintGoundMaxVelocity, moveParams);
            }
            else
            {
                return AccelerateVelocity(_movementData.CrouchGroundAccelerate, _movementData.CrouchGroundMaxVelocity, moveParams);
            }
        }

        public Vector3 MoveAir(CharacterMath.MoveParams moveParams)
        {
            return AccelerateVelocity(_movementData.AirAccelerate, _movementData.AirMaxVelocity, moveParams);
        }

        private Vector3 AccelerateVelocity(float min, float max, CharacterMath.MoveParams moveParams)
        {
            return CharacterMath.Accelerate(moveParams, min, max, Time.fixedDeltaTime);
        }

        #endregion

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(transform.position + Vector3.up, ActualMoveDirection);

            if (!OnSlope()) { return; }

            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, transform.position + _slopeHit.normal * 3f);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + CharacterMath.Project(_moveDirection, _slopeHit.normal) * 3f);
        }
    }
}