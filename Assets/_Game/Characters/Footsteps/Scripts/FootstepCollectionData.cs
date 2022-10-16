using System.Collections.Generic;
using UnityEngine;

namespace LOK1game.Character.Generic
{
    [CreateAssetMenu(fileName = "new Footstep Collection", menuName = "Create Footstep Collection")]
    public class FootstepCollectionData : ScriptableObject
    {
        public string TerrainLayerName => _terrainLayerName; //Gleb podimakhin iz irkutska loshara kanala net, no v kanave est' Gleb
        public Footstep FootstepPrefab => _footstepPrefab;
        public List<AudioClip> FootstepsClips => _footstepsClips;
        public AudioClip JumpClip => _jumpClip;
        public AudioClip LandClip => _landClip;

        [SerializeField] private string _terrainLayerName = "None";
        [SerializeField] private Footstep _footstepPrefab;

        [Space]
        [SerializeField] private List<AudioClip> _footstepsClips;
        [SerializeField] private AudioClip _jumpClip;
        [SerializeField] private AudioClip _landClip;
    }
}