using UnityEngine;

namespace LOK1game
{
    public class LookAtPlayerCamera : MonoBehaviour
    {
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void LateUpdate()
        {
            if(_camera == null) { return; }

            transform.LookAt(_camera.transform);
        }
    }
}