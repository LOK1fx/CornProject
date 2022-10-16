using System.Collections.Generic;
using UnityEngine;

namespace LOK1game.Tools
{
    public static class AudioSourceExtensions
    {
        public static void PlayRandomClipOnce(this AudioSource audio, AudioClip[] clips)
        {
            var index = Random.Range(0, clips.Length);

            audio.PlayOneShot(clips[index]);
        }

        public static void PlayRandomClipOnce(this AudioSource audio, List<AudioClip> clips)
        {
            PlayRandomClipOnce(audio, clips.ToArray());
        }
    }
}