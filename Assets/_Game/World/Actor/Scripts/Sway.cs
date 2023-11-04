using UnityEngine;

namespace LOK1game
{
    public class Sway : MonoBehaviour
    {
        [SerializeField] private float _speed = 2f;
        [SerializeField] private float _returnSpeed = 7f;

        [Space]
        [SerializeField] private Vector3 _multiplier = Vector3.one;

        private Quaternion _startRotation;
        private Vector2 _inputDelta;
        private float _playerSensitivity;

        private void Start()
        {
            _startRotation = transform.localRotation;
            _playerSensitivity = Settings.GetSensivity();
        }

        private void Update()
        {
            if (Cursor.lockState == CursorLockMode.Locked)
                _inputDelta = new Vector2(Input.GetAxis("Mouse X") * _playerSensitivity, Input.GetAxis("Mouse Y") * _playerSensitivity);
            else
                _inputDelta = Vector2.zero;

            UpdateSway();
        }

        private void UpdateSway()
        {
            var targetRotation = _startRotation * GetInputAdjustment();

            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * _returnSpeed);
        }

        private Quaternion GetInputAdjustment()
        {
            var inputAdjustmentX = Quaternion.AngleAxis(-_speed * _inputDelta.x * _multiplier.x, Vector3.up);
            var inputAdjustmentY = Quaternion.AngleAxis(_speed * _inputDelta.y * _multiplier.y, Vector3.right);
            var inputAdjustmentZ = Quaternion.AngleAxis(_speed * _inputDelta.x * _multiplier.z, Vector3.forward);

            return inputAdjustmentX * inputAdjustmentY * inputAdjustmentZ;
        }
    }
}