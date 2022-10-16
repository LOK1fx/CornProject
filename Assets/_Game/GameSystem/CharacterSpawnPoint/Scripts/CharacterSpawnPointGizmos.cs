using UnityEngine;

namespace LOK1game.Editor
{
    [RequireComponent(typeof(CharacterSpawnPoint))]
    public class CharacterSpawnPointGizmos : MonoBehaviour
    {
#if UNITY_EDITOR

        [SerializeField] private Color _capsuleColor = Color.green;
        [SerializeField] private Mesh _capsuleMesh;
        [SerializeField] private Vector3 _capsuleScale;
        [SerializeField] private Vector3 _capsulePositionOffset;

        private CharacterSpawnPoint _spawn;

        private void Awake()
        {
            _spawn = GetComponent<CharacterSpawnPoint>();
        }

        private void OnDrawGizmos()
        {
            if(_capsuleMesh != null)
            {
                Gizmos.color = _capsuleColor;
                Gizmos.DrawWireMesh(_capsuleMesh, transform.position + _capsulePositionOffset, Quaternion.identity, _capsuleScale);
            }

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position + Vector3.up * (Constants.Gameplay.PLAYER_HEIGHT * 0.5f),
                new Vector3(1, Constants.Gameplay.PLAYER_HEIGHT, 1));
        }

#endif
    }
}