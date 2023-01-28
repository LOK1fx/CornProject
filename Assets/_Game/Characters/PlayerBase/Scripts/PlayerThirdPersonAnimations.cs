using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LOK1game.Tools;

namespace LOK1game.Character
{
    [RequireComponent(typeof(Animator))]
    public class PlayerThirdPersonAnimations : MonoBehaviour
    {
        [SerializeField] private Player.Player _player;

        [Space]
        [SerializeField] private float _smoothAmount = 0.1f;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _player.Movement.OnJump += OnPlayerJump;
            _player.Movement.OnLand += OnPlayerLand;
        }

        private void OnDestroy()
        {
            _player.Movement.OnJump -= OnPlayerJump;
            _player.Movement.OnLand -= OnPlayerLand;
        }

        private void OnPlayerLand()
        {
            _animator.SetTrigger("Land");
        }

        private void OnPlayerJump()
        {
            _animator.SetTrigger("Jump");
        }

        private void LateUpdate()
        {
            var axis = _player.Movement.GetInputMoveAxis();

            _animator.SetFloat("X", axis.x, _smoothAmount, Time.deltaTime);
            _animator.SetFloat("Y", axis.y, _smoothAmount, Time.deltaTime);
            _animator.SetBool("OnGround", _player.State.OnGround);
        }
    }
}