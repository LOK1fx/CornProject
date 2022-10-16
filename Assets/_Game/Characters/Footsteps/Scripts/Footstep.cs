using UnityEngine;
using LOK1game.Tools;

namespace LOK1game.Character.Generic
{
    [RequireComponent(typeof(AudioSource))]
    public class Footstep : MonoBehaviour
    {
        [SerializeField] private float _lifetime = 0.6f;

        private AudioSource _source;
        private FootstepCollectionData _data;

        public void Construct(FootstepCollectionData data)
        {
            _data = data;
        }

        public void PlaySound()
        {
            _source.PlayRandomClipOnce(_data.FootstepsClips);
        }

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
        }

        private void Start()
        {
            Destroy(gameObject, _lifetime);
        }
    }
}