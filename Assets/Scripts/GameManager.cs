using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public GameSettingsSO gameSettings;
    public GameEventEmpty onSpawnEvent;
    public GameEventGameSettings onGameSettingsChangeEvent;
    public GameEventInt onPointsUpdateEvent;
    public GameEventInt onUITimerEvent;
    public GameEventInt onGameOverEvent;
    public GameEventInt onStartGameEvent;

    private float _levelTimer;
    private int _levelTimerInt => Mathf.FloorToInt(_levelTimer);
    private float _spawnTimer;
    private float _uiTimer;
    private bool finished;

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
        StartGame();
    }

    public void StartGame()
    {
        RestartSpawnTimer();
        _playerPoints = 0;
        _levelTimer = gameSettings.levelTime;

        onStartGameEvent?.Trigger(_levelTimerInt);
        onGameSettingsChangeEvent?.Trigger(gameSettings);
        finished = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (finished)
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
            finished = true;
            onGameOverEvent?.Trigger(_playerPoints);
        }
    }

    void RestartSpawnTimer()
    {
        _spawnTimer = Random.Range(gameSettings.minSpawnTime, gameSettings.maxSpawnTime);
    }

    // Called through the event system
    public void EdibleEaten(Edible edible)
    {
        playerPoints += edible.data.points;
    }
}
