using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameMusicManager : MonoBehaviour
{
    [Header("Background music tracks")]
    public List<AudioClip> preGameTracks;
    public List<AudioClip> inGameTracks;
    public List<AudioClip> gameOverTracks;

    // Used to queue music, in case the user stays in either in the pregame or game over state for longer than expected
    private Queue<AudioClip> _musicQueue = new();
    private bool _queueActive;
    
    private AudioSource _audioSource;

    // Used for shuffling
    private System.Random _random;

    // Used to transition between in-game and game over music
    private bool _fadeOut;
    private float _fadeOutduration;
    private float _fadeOutEllapsedTime;
    private float _originalSourceVolume;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _originalSourceVolume = _audioSource.volume;
        _random = new System.Random();
        
        // We want this manager to be configured once in the main scene and then be reused, so make sure we are
        // not destroying it
        DontDestroyOnLoad(this);
    }
    
    void Update()
    {
        if (_fadeOut)
            FadeOutStep();

        // When our queue is active and the music has finished, we need to move on to the next track
        if (_queueActive && !_audioSource.isPlaying)
        {
            NextTrack();
        }
    }

    void FadeOutStep()
    {
        _fadeOutEllapsedTime += Time.deltaTime;
        var step = _fadeOutEllapsedTime / _fadeOutduration;
        _audioSource.volume = Mathf.Lerp(_originalSourceVolume, 0, step);
    }

    private void NextTrack()
    {
        // In cases when our queue is a single clip, then we need to make sure to not break it
        if (_audioSource.clip != null)
            _musicQueue.Enqueue(_audioSource.clip);
        
        _audioSource.clip = _musicQueue.Dequeue();
        _audioSource.volume = _originalSourceVolume;
        _audioSource.Play();
        
    }
    
    // Hooked up to the GameOpen event, allowing the manager to play music in the main menu
    public void PreGame()
    {
        _audioSource.Stop();
        _audioSource.clip = null;
        SetupQueue(preGameTracks);
    }

    // Hooked up to the Game Start event
    public void StartGame(int _)
    {
        _queueActive = false;
        _fadeOut = false;
        _fadeOutEllapsedTime = 0;
        _audioSource.Stop();
        _audioSource.clip = inGameTracks.PickRandom();
        _audioSource.volume = _originalSourceVolume;
        _audioSource.Play();
    }

    public void Countdown(int fadeOutSeconds)
    {
        _fadeOut = true;
        _fadeOutduration = fadeOutSeconds;
    }
    
    // Hooked up to the game over event
    public void GameOver(int _)
    {
        _fadeOut = false;
        _fadeOutEllapsedTime = 0;
        _audioSource.Stop();
        _audioSource.clip = null;
        SetupQueue(gameOverTracks);
    }

    // Shuffle the tracks in the provided list and then reset the music queue with it.
    private void SetupQueue(List<AudioClip> trackSource)
    {
        _musicQueue = new (trackSource.OrderBy(t => _random.Next()));
        NextTrack();
        _queueActive = true;
    }
}
