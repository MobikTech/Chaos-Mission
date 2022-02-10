using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace ChaosMission.Audio
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private Track[] _menuMusics;
        [SerializeField] private Track[] _levelMusics;
        
        private Track[] _currentPlayList;

        private AudioSource _audioSource;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }
        
        private void Update()
        {
            if (_audioSource.clip == null)
            {
                return;
            }

            if (!_audioSource.isPlaying)
            {
                PlayRandomTrack();
            }
        }
        
        
        private void PlayRandomTrack()
        {
            SetTrackSettings(_currentPlayList[Random.Range(0, _currentPlayList.Length)]);
            _audioSource.Play();
        }
        
        private void SetTrackSettings(Track track)
        {
            _audioSource.clip = track.Clip;
            _audioSource.pitch = track.Pitch;
            _audioSource.volume = track.Volume;
        }
      
        private void SetPlayList(Track[] tracks)
        {
            _currentPlayList = tracks;
            _audioSource.Stop();
            PlayRandomTrack();
        }

        private void OnActiveSceneChanged(Scene current, Scene next)
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
    }
}
