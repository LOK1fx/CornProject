using UnityEngine;

namespace LOK1game.DebugTools
{
    [RequireComponent(typeof(BoxCollider))]
    public class BoxColliderGizmo : MonoBehaviour
    {
        [SerializeField] private Color _gizmosColor = Color.green;
        [SerializeField] private bool _wire = true;

        private BoxCollider _collider;

        private void OnValidate()
        {
            if(_collider) { return; }

            _collider = GetComponent<BoxCollider>();
        }

        private void OnDrawGizmos()
        {
            var center = transform.position + _collider.center;
            var scale = transform.localScale;
            var size = new Vector3(scale.x * _collider.size.x,
                scale.y * _collider.size.y, scale.z * _collider.size.z);

            Gizmos.color = _gizmosColor;

            if(_wire)
            {
                Gizmos.DrawWireCube(center, size);
            }
            else
            {
                Gizmos.DrawCube(center, size);
            }
        }
    }
}