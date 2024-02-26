using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
    public GameSettingsSO gameSettings;
    
    [Header("Game Events")]
    public GameEventEmpty onSpawnEvent;
    public GameEventGameSettings onGameSettingsChangeEvent;
    public GameEventInt onPointsUpdateEvent;
    public GameEventInt onUITimerEvent;
    public GameEventInt onGameOverEvent;
    public GameEventInt onStartGameEvent;
    public GameEventInt onCountdownEvent;

    private float _levelTimer;
    private int _levelTimerInt => Mathf.FloorToInt(_levelTimer);
    private float _spawnTimer;
    private float _uiTimer;
    private bool _finished;
    
    // Used to make sure we only fire the event once
    private bool _countdownEventFired;

    private AudioSource _audioSource;

    public int playerPoints
    {
        get => _playerPoints;
        set
        {
            _playerPoints = value;
            onPointsUpdateEvent?.Trigger(_playerPoints);
        }
    }

    private int _playerPoints;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        StartGame();
    }

    public void StartGame()
    {
        RestartSpawnTimer();
        _playerPoints = 0;
        _levelTimer = gameSettings.levelTime;

        onStartGameEvent?.Trigger(_levelTimerInt);
        onGameSettingsChangeEvent?.Trigger(gameSettings);
        _finished = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_finished)
            return;
        
        _spawnTimer -= Time.deltaTime;
        _levelTimer -= Time.deltaTime;
        _uiTimer -= Time.deltaTime;
        
        if (_spawnTimer <= 0)
        {
            onSpawnEvent?.Trigger(null);
            RestartSpawnTimer();
        }

        if (_uiTimer <= 0)
        {
            onUITimerEvent?.Trigger(_levelTimerInt);
            _uiTimer = 1;
        }

        if (_levelTimer <= 0)
        {
            _finished = true;
            onGameOverEvent?.Trigger(_playerPoints);
        }

        if (!_countdownEventFired && _levelTimer <= gameSettings.countdownEventSeconds)
        {
            onCountdownEvent?.Trigger(gameSettings.countdownEventSeconds);
            _countdownEventFired = true;
        }
    }

    void RestartSpawnTimer()
    {
        _spawnTimer = Random.Range(gameSettings.minSpawnTime, gameSettings.maxSpawnTime);
    }

    // Called through the event system
    public void EdibleEaten(Edible edible)
    {
        playerPoints += edible.points;
    }
}
