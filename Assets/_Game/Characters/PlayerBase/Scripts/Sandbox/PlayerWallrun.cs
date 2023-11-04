using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LOK1game.Player;

namespace LOK1game.recode.Player
{
	public class PlayerWallrun : MonoBehaviour, IPawn
	{
        [SerializeField] private LayerMask _wallsMask;

        [SerializeField] private float _tilt;
        [SerializeField] private float _gravity;
        [SerializeField] private float _wallDistance = 0.6f;
        [SerializeField] private float _minJumpHeight = 2f;

        [SerializeField] private bool _proMode;

        private bool _wallOnLeft;
        private bool _wallOnRight;

        private Vector2 _iAxis;

        private RaycastHit _leftWallHit;
        private RaycastHit _rightWallHit;

        private Rigidbody _rb;
        private LOK1game.Player.Player _player;

        private bool _isWallruning;

        public Controller Controller => _controller;

        private Controller _controller;

        private float _targetTilt;

        private void Awake() => _rb = GetComponent<Rigidbody>();

        private void Start()
        {
            _player = GetComponent<LOK1game.Player.Player>();
        }

        private void CheckWall()
        {
            var direction = _player.Movement.DirectionTransform;

            if (_proMode)
            {
                if (_iAxis.x >= 0.3f)
                {
                    _wallOnRight = Physics.Raycast(direction.position, direction.right, out _rightWallHit, _wallDistance, _wallsMask);

                    _wallOnLeft = false;

                    Debug.DrawRay(transform.position + Vector3.up, transform.right * 2f, Color.green);
                }
                else if (_iAxis.x <= -0.3f)
                {
                    _wallOnLeft = Physics.Raycast(direction.position, -direction.right, out _leftWallHit, _wallDistance, _wallsMask);

                    _wallOnRight = false;

                    Debug.DrawRay(transform.position + Vector3.up, -transform.right * 2f, Color.green);
                }
                else
                {
                    _wallOnRight = false;
                    _wallOnLeft = false;
                }
            }
            else
            {
                _wallOnRight = Physics.Raycast(direction.position, direction.right, out _rightWallHit, _wallDistance, _wallsMask);
                _wallOnLeft = Physics.Raycast(direction.position, -direction.right, out _leftWallHit, _wallDistance, _wallsMask);
            }
        }

        private void Update()
        {
            _player.Camera.Tilt = Mathf.Lerp(_player.Camera.Tilt, _targetTilt, Time.deltaTime * 8f);

            CheckWall();
        }


        private void FixedUpdate()
        {
            if (CanWallRun())
            {
                if (_wallOnLeft)
                {
                    StartWallrun();
                }
                else if (_wallOnRight)
                {
                    StartWallrun();
                }
                else
                {
                    StopWallrun();
                }
            }
            else
            {
                StopWallrun();
            }
        }

        private void JumpFromWall()
        {
            if(!_isWallruning) { return; }

            if (_wallOnLeft)
            {
                var dir = transform.up + _leftWallHit.normal;

                _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

                _rb.AddForce(dir * 25f, ForceMode.Impulse);
            }
            else if (_wallOnRight)
            {
                var dir = transform.up + _rightWallHit.normal;

                _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

                _rb.AddForce(dir * 25f, ForceMode.Impulse);
            }
        }

        private void StartWallrun()
        {
            _isWallruning = true;

            _rb.useGravity = false;

            _rb.AddForce(Vector3.down * _gravity, ForceMode.Force);
            _rb.AddForce(_player.Movement.DirectionTransform.forward * 17f, ForceMode.Force);

            if (_wallOnLeft)
                _targetTilt = -_tilt;
            else if (_wallOnRight)
                _targetTilt = _tilt;
        }

        private void StopWallrun()
        {
            _rb.useGravity = true;

            _isWallruning = false;

            _targetTilt = 0;
        }

        private bool CanWallRun()
        {
            if(_proMode)
            {
                if (_iAxis.x == 0) { return false; }
            }

            Debug.DrawRay(transform.position + Vector3.up, Vector3.down * _minJumpHeight, Color.green);

            return !Physics.Raycast(transform.position + Vector3.up, Vector3.down, _minJumpHeight, _wallsMask);
        }

        public void OnPocces(Controller sender)
        {
            _controller = sender;
        }

        public void OnUnpocces()
        {
            _controller = null;
        }

        public void OnInput(object sender)
        {
            _iAxis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (Input.GetKeyDown(KeyCode.Space))
                JumpFromWall();
        }
    }
}