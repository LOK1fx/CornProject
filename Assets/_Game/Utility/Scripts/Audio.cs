using System.Collections.Generic;
using UnityEngine;

namespace LOK1game.Tools
{
    public static class Audio
    {
        public static AudioClip GetRandomClip(AudioClip[] clips)
        {
            var index = Random.Range(0, clips.Length);

            return clips[index];
        }

        public static AudioClip GetRandomClip(List<AudioClip> clips)
        {
            return GetRandomClip(clips.ToArray());
        }

        public static float GetRandomPitch(float min, float max)
        {
            return Random.Range(min, max);
        }
    }
}
