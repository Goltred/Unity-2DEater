using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
    public GameSettingsSO gameSettings;
    
    [Header("Game Events")]
    [Tooltip("Triggered when the spawn timer runs out, notifying others that food needs to be spawned")]
    public GameEventEmpty onSpawnEvent;
    [Tooltip("Triggered when the game is starting to allow listeners to react to settings changes. The event includes the GameSettingsSO data")]
    public GameEventGameSettings onGameSettingsChangeEvent;
    [Tooltip("Triggered when points have been updated. The event includes the current points of the player")]
    public GameEventInt onPointsUpdateEvent;
    [Tooltip("Triggered every 1 second. The event includes the remaining time left in the level")]
    public GameEventInt onUITimerEvent;
    [Tooltip("Triggered when the level timer runs out. The event includes the current points of the player")]
    public GameEventInt onGameOverEvent;
    [Tooltip("Triggered when the StartGame() method is called. The event includes the time left in the level")]
    public GameEventInt onStartGameEvent;
    [Tooltip("Triggered when the level timer has a set amount of seconds left. This includes the number of seconds until the timer runs out")]
    public GameEventInt onCountdownEvent;

    private float _levelTimer;
    private int _levelTimerInt => Mathf.FloorToInt(_levelTimer);
    private float _spawnTimer;
    private float _uiTimer;
    private bool _finished;
    
    // Used to make sure we only fire the event once
    private bool _countdownEventFired;

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

    // We want to make sure we do start the game once this object is created
    void Start()
    {
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
    
    void Update()
    {
        // Exit early if we are not actively playing
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

        // We trigger this every one second to allow listeners to update themselves if required (e.g. UI system)
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

        // Hook up our countdown event here based on the desired settings. This allows other systems
        // to perform their actions in the configured time
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
