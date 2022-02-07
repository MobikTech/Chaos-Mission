using System;
using System.Collections;
using System.Collections.Generic;
using ChaosMission.Spawners;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace ChaosMission
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private Track[] _menuMusics;
        [SerializeField] private Track[] _levelMusics;
        private List<Track> _currentPlayList;
        private Track _currentTrack;
        
        private void Awake()
        {
            InitializeMusics(_menuMusics);
            InitializeMusics(_levelMusics);
            SceneManager.activeSceneChanged += ChangedActiveScene;
        }
        

        private void InitializeMusics(Track[] musics)
        {
            foreach (var music in musics)
            {
                music.source = gameObject.AddComponent<AudioSource>();

                music.source.clip = music.clip;
                music.source.pitch = music.pitch;
                music.source.volume = music.volume;
            }
        }

        private void PlayRandomTrack(IList<Track> tracks)
        {
            _currentTrack = tracks[Random.Range(0, tracks.Count)];
            _currentTrack.source.Play();
        }
        
        private void ChangedActiveScene(Scene current, Scene next)
        {
            if (next.buildIndex == 0)
            {
                _currentPlayList = new List<Track>(_menuMusics);
            }
            else
            {
                _currentPlayList = new List<Track>(_levelMusics);
            }
        }

        void Update()
        {
            if (_currentTrack.source.isPlaying)
                PlayRandomTrack(_currentPlayList);
        }
    }
}
