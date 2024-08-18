using LOK1game.PlayerDomain;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game.Character.Generic
{
    [RequireComponent(typeof(PlayerState))]
    public class CharacterFootsteps : MonoBehaviour
    {
        public float ClipsRate = 0.25f;

        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private List<FootstepCollectionData> _footstepsCollection = new List<FootstepCollectionData>();

        private FootstepCollectionData _currentData;
        private PlayerState _state;
        private float _clipTimer;

        private void Awake()
        {
            _currentData = _footstepsCollection[0];
            _state = GetComponent<PlayerState>();
        }

        private void Update()
        {
            if (_state.OnGround == false || _state.IsMoving() == false)
                return;

            _clipTimer += Time.deltaTime;

            if(_clipTimer >= ClipsRate)
            {
                if(Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.down, out var hit, 1f, _groundLayer, QueryTriggerInteraction.Collide))
                {
                    if(hit.collider.gameObject.TryGetComponent<Terrain>(out var terrain))
                    {
                        var terrainChecker = new TerrainChecker(terrain);

                        foreach (var data in _footstepsCollection)
                        {
                            if(data.TerrainLayerName == terrainChecker.GetLayerName(transform.position))
                            {
                                _currentData = data;
                            }
                        }
                    }
                    else
                    {
                        _currentData = _footstepsCollection[0];
                    }

                    var footstep = Instantiate(_currentData.FootstepPrefab, hit.point, Quaternion.identity);
                    footstep.Construct(_currentData);
                    footstep.PlaySound();
                }

                _clipTimer = 0;
            }
        }
    }
}