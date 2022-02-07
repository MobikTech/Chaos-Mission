using UnityEngine;

namespace ChaosMission
{
    [System.Serializable]
    public class Track
    {
        public AudioClip clip;

        [Range(0f, 1f)] public float volume;
        [Range(.1f, 3f)] public float pitch;


        [HideInInspector] public AudioSource source;
    }
}