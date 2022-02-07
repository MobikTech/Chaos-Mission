using UnityEngine;

namespace ChaosMission.Audio
{
    [System.Serializable]
    public class Track
    {
        public AudioClip Clip;

        [Range(0f, 1f)] public float Volume = 0.5f;
        [Range(.1f, 3f)] public float Pitch = 0.5f;


        // [HideInInspector] public AudioSource source;
    }
}