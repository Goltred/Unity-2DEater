using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Level Settings")]
    public float LevelTime = 60f;

    [Header("Spawn Settings")]
    public float MinSpawnTime = 3f;
    public float MaxSpawnTime = 6f;
    public GameEventEmpty OnSpawnEvent;
    
    private float spawnTimer;
    private float uiTimer;

    void Start()
    {
        RestartSpawnTimer();
        // Reduce the initial spawn timer so that we start playing early but still with some randomness
        spawnTimer -= 3;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        LevelTime -= Time.deltaTime;
        uiTimer -= Time.deltaTime;
        
        if (spawnTimer <= 0)
        {
            OnSpawnEvent?.Trigger(null);
            RestartSpawnTimer();
        }

        if (uiTimer <= 0)
        {
            //OnUIRefreshEvent?.Trigger();
            uiTimer = 1;
        }

        if (LevelTime <= 0)
        {
            //OnEndGameEvent?.Trigger();
        }
    }

    void RestartSpawnTimer()
    {
        spawnTimer = Random.Range(MinSpawnTime, MaxSpawnTime);
    }
}
