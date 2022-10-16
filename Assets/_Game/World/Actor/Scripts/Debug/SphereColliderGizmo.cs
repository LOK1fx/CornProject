using UnityEngine;

namespace LOK1game.DebugTools
{
    [RequireComponent(typeof(SphereCollider))]
    public class SphereColliderGizmo : MonoBehaviour
    {
        [SerializeField] private Color _gizmosColor = Color.green;
        [SerializeField] private bool _wire = true;

        private SphereCollider _collider;

        private void OnValidate()
        {
            if (_collider) { return; }

            _collider = GetComponent<SphereCollider>();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmosColor;

            var radius = _collider.radius;
            var position = transform.position + _collider.center;

            if (_wire)
            {
                Gizmos.DrawWireSphere(position, radius);
            }
            else
            {
                Gizmos.DrawSphere(position, radius);
            }
        }
    }
}