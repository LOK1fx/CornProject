using System.Collections;
using UnityEngine;
using Photon.Pun;
using System;
using LOK1game.Weapon;
using Cinemachine;
using LOK1game.Character.Generic;

namespace LOK1game.Player
{
    [RequireComponent(typeof(PlayerMovement), typeof(PlayerCamera), typeof(PlayerState))]
    [RequireComponent(typeof(Health), typeof(PlayerWeapon))]
    public class Player : Pawn, IDamagable
    {
        public event Action OnRespawned;
        public event Action OnDeath;

        public PlayerWeapon Weapon { get; private set; }
        public PlayerMovement Movement { get; private set; }
        public PlayerCamera Camera { get; private set; }
        public PlayerState State { get; private set; }
        public FirstPersonArms FirstPersonArms => _firstPersonArms;
        public Health Health { get; private set; }
        public bool IsDead { get; private set; }

        [SerializeField] private GameObject[] _localOnlyObjects;
        [SerializeField] private GameObject[] _worldOnlyObjects;
        [SerializeField] private FirstPersonArms _firstPersonArms;
        [SerializeField] private Ragdoll _ragdoll;
        [SerializeField] private GameObject _visual;
        [SerializeField] private GameObject _playerInfoRoot;
        [SerializeField] private Vector3 _crouchEyePosition;

        private Vector3 _defaultEyePosition;

        [Space]
        [SerializeField] private GameObject _freeCameraPrefab;
        [SerializeField] private float _respawnTime;

        private void Awake()
        {
            Health = GetComponent<Health>();
            Movement = GetComponent<PlayerMovement>();
            Camera = GetComponent<PlayerCamera>();
            Camera.Construct(this);
            State = GetComponent<PlayerState>();
            Weapon = GetComponent<PlayerWeapon>();
            Weapon.Construct(this);

            Movement.OnLand += OnLand;
            Movement.OnJump += OnJump;
            Movement.OnStartCrouch += OnStartCrouching;
            Movement.OnStopCrouch += OnStopCrouching;

            _defaultEyePosition = Camera.GetCameraTransform().localPosition;
        }

        private void OnDestroy()
        {
            Movement.OnLand -= OnLand;
            Movement.OnJump -= OnJump;
            Movement.OnStartCrouch -= OnStartCrouching;
            Movement.OnStopCrouch -= OnStopCrouching;
        }

        private void Start()
        {
            if(IsLocal == false)
            {
                gameObject.layer = 7;
                Movement.Rigidbody.isKinematic = true;
                playerType = EPlayerType.World;

                foreach (var gameObject in _localOnlyObjects)
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                _playerInfoRoot.SetActive(false);
                playerType = EPlayerType.View;

                foreach (var gameObject in _worldOnlyObjects)
                {
                    gameObject.SetActive(false);
                }
            }
        }

        private void Update()
        {
            var cameraRotation = Camera.GetCameraTransform().eulerAngles;

            Movement.DirectionTransform.rotation = Quaternion.Euler(0f, cameraRotation.y, 0f);
        }

        public override void OnInput(object sender)
        {
            if (IsLocal == false || IsDead)
                return;

            var inputAxis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            Camera.OnInput(this);
            Movement.SetAxisInput(inputAxis);
            Weapon.OnInput(this);

            if (Input.GetKey(KeyCode.Space))
                Movement.Jump();

            if(Input.GetKeyDown(KeyCode.LeftControl))
                Movement.StartCrouch();

            if(Input.GetKeyUp(KeyCode.LeftControl))
                if(Movement.CanStand())
                    Movement.StopCrouch();

            if(Input.GetKeyDown(KeyCode.K))
                photonView.RPC(nameof(RemoveHealth), RpcTarget.All, new object[1] { Health.MaxHp });

            if(Input.GetKeyDown(KeyCode.U))
                photonView.RPC(nameof(RemoveHealth), RpcTarget.All, new object[1] { 15 });
        }

        private void OnLand()
        {
            Camera.AddCameraOffset(Vector3.down * 0.5f);
        }

        private void OnJump()
        {
            Camera.AddCameraOffset(Vector3.up * 0.35f);
        }

        private void OnStartCrouching()
        {
            Camera.DesiredPosition = _crouchEyePosition;
        }

        private void OnStopCrouching()
        {
            Camera.DesiredPosition = _defaultEyePosition;
        }

        public void TakeDamage(Damage damage)
        {
            if (IsDead)
                return;

            var text = new PopupTextParams($"Damage: {damage.Value}", 5f, Color.red);

            PopupText.Spawn<PopupText3D>(transform.position + Vector3.up * 2, transform, text);

            photonView.RPC(nameof(RemoveHealth), RpcTarget.All, new object[1] { damage.Value });
        }

        [PunRPC]
        private void AddHealth(int value)
        {
            Health.AddHealth(value);
        }

        [PunRPC]
        private void RemoveHealth(int value)
        {
            Health.ReduceHealth(value);

            if(Health.Hp <= 0)
                photonView.RPC(nameof(Death), RpcTarget.All);
        }

        [PunRPC]
        private void SetHealth(int value)
        {
            Health.SetHealth(value);
        }

        [PunRPC]
        private void Death()
        {
            if (IsDead)
                return;

            if(IsLocal)
                StartCoroutine(FreecamRoutine());

            _ragdoll.ActivateRagdoll(Movement.Rigidbody.velocity);
            _ragdoll.transform.SetParent(null);

            IsDead = true;
            Movement.Rigidbody.isKinematic = true;
            Movement.PlayerCollider.enabled = false;

            _visual.SetActive(false);
            _playerInfoRoot.SetActive(false);

            OnDeath?.Invoke();

            if(IsLocal)
            {
                var respawnPosition = GetRandomSpawnPosition(true);

                photonView.RPC(nameof(Respawn), RpcTarget.All, new object[3] { respawnPosition.x, respawnPosition.y, respawnPosition.z });
            }
        }

        private IEnumerator FreecamRoutine()
        {
            var cameraTransform = Camera.GetCameraTransform();
            var freeCamera = Instantiate(_freeCameraPrefab, cameraTransform.position, cameraTransform.rotation);

            if (freeCamera.TryGetComponent<Rigidbody>(out var rigidbody))
                rigidbody.velocity = Movement.Rigidbody.velocity;

            yield return new WaitForSeconds(_respawnTime);

            freeCamera.GetComponent<CinemachineVirtualCamera>().Priority = 0;
            Destroy(freeCamera, 0.5f);
        }

        [PunRPC]
        private void Respawn(float respawnPositionX, float respawnPositionY, float respawnPositionZ) //Photon RPC don't serialize/deserialize Vector3 type
        {
            var respawnPosition = new Vector3(respawnPositionX, respawnPositionY, respawnPositionZ);

            Debug.DrawRay(respawnPosition, Vector3.up * 2f, Color.yellow, _respawnTime + 1f, false);

            StartCoroutine(RespawnRoutine(respawnPosition));
        }

        private IEnumerator RespawnRoutine(Vector3 respawnPosition)
        {
            yield return new WaitForSeconds(_respawnTime);

            IsDead = false;

            if(IsLocal)
            {
                Movement.Rigidbody.isKinematic = false;
            }
            else
            {
                _playerInfoRoot.SetActive(true);
            }

            Movement.PlayerCollider.enabled = true;
            Movement.Rigidbody.velocity = Vector3.zero;

            if (State.IsCrouching)
                Movement.StopCrouch();

            Health.ResetHealth();

            _visual.SetActive(true);
            transform.position = respawnPosition;

            _ragdoll.DeactivateRagdoll();
            _ragdoll.transform.SetParent(_visual.transform);

            OnRespawned?.Invoke();
        }
    }
}