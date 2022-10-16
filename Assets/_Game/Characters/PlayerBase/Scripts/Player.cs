using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LOK1game.Tools;
using Photon.Pun;

namespace LOK1game.Player
{
    [RequireComponent(typeof(PlayerMovement), typeof(PlayerCamera), typeof(PlayerState))]
    public class Player : Pawn, IDamagable
    {
        public PlayerMovement Movement { get; private set; }
        public PlayerCamera Camera { get; private set; }
        public PlayerState State { get; private set; }

        public int Health { get; private set; }
        public bool IsDead { get; private set; }

        [SerializeField] private int _maxHealth = 100;
        [SerializeField] private GameObject _vis; //TEST
        [SerializeField] private RectTransform _healthBar; //TEST

        private void Awake()
        {
            Movement = GetComponent<PlayerMovement>();
            Camera = GetComponent<PlayerCamera>();
            State = GetComponent<PlayerState>();

            Movement.OnLand += OnLand;
        }

        private void Start()
        {
            Health = _maxHealth;

            if(IsLocal == false)
            {
                gameObject.layer = 7;
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

            UpdateHealthBar();
        }

        [PunRPC]
        private void RemoveHealth(int value)
        {
            Health -= value;

            UpdateHealthBar();
        }

        [PunRPC]
        private void SetHealth(int value)
        {
            Health = value;

            UpdateHealthBar();
        }

        private void UpdateHealthBar()
        {
            _healthBar.localScale = new Vector3(Health * 0.01f, 1f, 1f);
        }

        [PunRPC]
        private void Death(int damage)
        {
            if (IsDead)
                return;

            IsDead = true;
            Movement.Rigidbody.isKinematic = true;
            Movement.PlayerCollider.enabled = false;

            _vis.SetActive(false);

            Invoke(nameof(Revive), 4f);
        }

        [PunRPC]
        private void Revive()
        {
            IsDead = false;
            Movement.Rigidbody.isKinematic = false;
            Movement.PlayerCollider.enabled = true;
            Movement.Rigidbody.velocity = Vector3.zero;

            photonView.RPC(nameof(SetHealth), RpcTarget.All, new object[1] { _maxHealth });

            _vis.SetActive(true);

            transform.position = GetRandomSpawnPosition(true);
        }
    }
}