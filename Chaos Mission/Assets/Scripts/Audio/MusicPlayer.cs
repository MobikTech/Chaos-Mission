using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace ChaosMission.Audio
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private Track[] _menuMusics;
        [SerializeField] private Track[] _levelMusics;
        
        private List<Track> _currentPlayList;

        private AudioSource _audioSource;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            SceneManager.activeSceneChanged += ChangedActiveScene;
            
        }
        

        private void SetTrack(Track track)
        {
            _audioSource.clip = track.Clip;
            _audioSource.pitch = track.Pitch;
            _audioSource.volume = track.Volume;
        }

        private void PlayRandomTrack(IList<Track> tracks)
        {
            SetTrack(tracks[Random.Range(0, tracks.Count)]);
            _audioSource.Play();
        }
        
        private void ChangedActiveScene(Scene current, Scene next)
        {
            if (next.buildIndex == 0)
            {
                SetPlayList(_menuMusics);
            }
            else
            {
                SetPlayList(_levelMusics);
            }
        }

        private void SetPlayList(Track[] tracks)
        {
            _currentPlayList = new List<Track>(tracks);
            _audioSource.Stop();
            PlayRandomTrack(_currentPlayList);
        }

        private void Update()
        {
            if(_audioSource.clip == null) return;
            if (!_audioSource.isPlaying)
                PlayRandomTrack(_currentPlayList);
        }
    }
}
