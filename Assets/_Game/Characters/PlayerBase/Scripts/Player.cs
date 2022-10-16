using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LOK1game.Tools;
using Photon.Pun;
using System;
using LOK1game.Weapon;

namespace LOK1game.Player
{
    [RequireComponent(typeof(PlayerMovement), typeof(PlayerCamera), typeof(PlayerState))]
    public class Player : Pawn, IDamagable
    {
        public event Action OnHealthChanged;

        public PlayerWeapon Weapon { get; private set; }
        public PlayerMovement Movement { get; private set; }
        public PlayerCamera Camera { get; private set; }
        public PlayerState State { get; private set; }
        public FirstPersonArms FirstPersonArms => _firstPersonArms;
        public int Health { get; private set; }
        public bool IsDead { get; private set; }

        [SerializeField] private FirstPersonArms _firstPersonArms;
        [SerializeField] private GameObject _visual;
        [SerializeField] private GameObject _playerInfoRoot;

        [Space]
        [SerializeField] private float _respawnTime;
        [SerializeField] private int _maxHealth = 100;

        private void Awake()
        {
            Movement = GetComponent<PlayerMovement>();
            Camera = GetComponent<PlayerCamera>();
            State = GetComponent<PlayerState>();
            Weapon = GetComponent<PlayerWeapon>();
            Weapon.Construct(this);

            Movement.OnLand += OnLand;
        }

        private void Start()
        {
            Health = _maxHealth;

            if(IsLocal == false)
            {
                gameObject.layer = 7;
            }
            else
            {
                _playerInfoRoot.SetActive(false);
            }
        }

        private void Update()
        {
            var cameraRotation = Camera.GetCameraTransform().eulerAngles;

            Movement.DirectionTransform.rotation = Quaternion.Euler(0f, cameraRotation.y, 0f);
        }

        public override void OnInput(object sender)
        {
            if (IsLocal == false)
                return;

            var inputAxis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            Camera.OnInput(this);

            if (IsDead)
                return;

            Movement.SetAxisInput(inputAxis);
            Weapon.OnInput(this);

            if (Input.GetKey(KeyCode.Space))
            {
                Movement.Jump();
            }

            if(Input.GetKeyDown(KeyCode.LeftControl))
            {
                Movement.StartCrouch();
            }
            if(Input.GetKeyUp(KeyCode.LeftControl))
            {
                Movement.StopCrouch();
            }

            if(Input.GetKeyDown(KeyCode.K))
            {
                photonView.RPC(nameof(Death), RpcTarget.All, new object[1] { _maxHealth });
            }
            if(Input.GetKeyDown(KeyCode.U))
            {
                photonView.RPC(nameof(RemoveHealth), RpcTarget.All, new object[1] { 15 });

                if(Health <= 0)
                {
                    photonView.RPC(nameof(Death), RpcTarget.All, new object[1] { 15 });
                }
            }
        }

        private void OnLand()
        {
            Camera.AddCameraOffset(Vector3.down);
        }

        public void TakeDamage(Damage damage)
        {
            if (IsDead)
                return;

            var text = new PopupTextParams($"Damage: {damage.Value}", 5f, Color.red);

            PopupText.Spawn<PopupText3D>(transform.position + Vector3.up * 2, transform, text);

            photonView.RPC(nameof(RemoveHealth), RpcTarget.All, new object[1] { damage.Value });

            if(Health <= 0)
            {
                photonView.RPC(nameof(Death), RpcTarget.All, new object[1] { damage.Value });
            }
        }

        [PunRPC]
        private void AddHealth(int value)
        {
            Health += value;

            OnHealthChanged?.Invoke();
        }

        [PunRPC]
        private void RemoveHealth(int value)
        {
            Health -= value;

            OnHealthChanged?.Invoke();
        }

        [PunRPC]
        private void SetHealth(int value)
        {
            Health = value;

            OnHealthChanged?.Invoke();
        }

        [PunRPC]
        private void Death(int damage)
        {
            if (IsDead)
                return;

            IsDead = true;
            Movement.Rigidbody.isKinematic = true;
            Movement.PlayerCollider.enabled = false;

            _visual.SetActive(false);

            var respawnPosition = GetRandomSpawnPosition(true);

            photonView.RPC(nameof(Respawn), RpcTarget.All, new object[3] { respawnPosition.x, respawnPosition.y, respawnPosition.z } );
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
            Movement.Rigidbody.isKinematic = false;
            Movement.PlayerCollider.enabled = true;
            Movement.Rigidbody.velocity = Vector3.zero;

            SetHealth(_maxHealth);

            _visual.SetActive(true);
            transform.position = respawnPosition;
        }
    }
}