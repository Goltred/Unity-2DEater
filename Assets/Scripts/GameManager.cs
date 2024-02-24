using Unity.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameSettingsSO gameSettings;
    public GameEventEmpty onSpawnEvent;
    public GameEventGameSettings onGameSettingsChangeEvent;

    private float _levelTimer;
    private float _spawnTimer;
    private float _uiTimer;

    [ReadOnly]
    public int playerPoints;

    void Start()
    {
        RestartSpawnTimer();
        // Reduce the initial spawn timer so that we start playing early but still with some randomness
        _spawnTimer -= 3;
        _levelTimer = gameSettings.levelTime;
        onGameSettingsChangeEvent?.Trigger(gameSettings);
    }

    // Update is called once per frame
    void Update()
    {
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
            //OnUIRefreshEvent?.Trigger();
            _uiTimer = 1;
        }

        if (_levelTimer <= 0)
        {
            //OnEndGameEvent?.Trigger();
        }
    }

    void RestartSpawnTimer()
    {
        _spawnTimer = Random.Range(gameSettings.minSpawnTime, gameSettings.maxSpawnTime);
    }

    public void EdibleEaten(Edible edible)
    {
        playerPoints += edible.data.points;
    }
}
