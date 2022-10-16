using UnityEngine;
using TMPro;

namespace LOK1game
{
    public class DistanceToPlayerTextMeshPro : MonoBehaviour
    {
        [SerializeField] private Transform _root;

        private Camera _playerCamera;
        private TextMeshPro _text;

        private void Start()
        {
            _text = GetComponent<TextMeshPro>();
            _playerCamera = Camera.main;
        }

        private void LateUpdate()
        {
            if(_playerCamera == null || _root == null) { return; }

            _text.text = Vector3.Distance(_root.position, _playerCamera.transform.position).ToString();
        }
    }
}