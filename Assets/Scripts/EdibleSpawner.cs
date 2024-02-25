using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EdibleSpawner : MonoBehaviour
{
    public List<Edible> ediblePrefabs = new();
    
    private Dictionary<int, Stack<Edible>> _ediblePool = new();
    private int _ediblesUpperLimit;
    private Vector3 _topRightField;
    private Vector3 _bottomLeftField;
    private Vector3 _outsideField;
    private GameSettingsSO _settings;

    private GameObject _ediblesParent;

    void Start()
    {
        _bottomLeftField = Camera.main.ViewportToWorldPoint(Vector3.zero);
        _topRightField = Camera.main.ViewportToWorldPoint(Vector3.one);
        _outsideField = _topRightField * 2;
        _ediblesUpperLimit = ediblePrefabs.Count - 1;

        _ediblesParent = new GameObject("Edibles");
    }

    public void ChangeSettings(GameSettingsSO newSettings)
    {
        _settings = newSettings;
    }

    public void Spawn()
    {
        // In order to offer randomness we pick the next edible randomly
        var randomIndex = Mathf.Clamp(Random.Range(0, _ediblesUpperLimit), 0, _ediblesUpperLimit);
        var choice = ediblePrefabs.Skip(randomIndex).First();

        var randomSpeed = Random.Range(_settings.minEdibleSpeed, _settings.maxEdibleSpeed);
        
        var spawnPos = RandomSpawnPosition(choice.GetRenderer());

        // To reduce instancing we check if we have an available copy of the edible we want to spawn and use that instead
        if (_ediblePool.TryGetValue(choice.data.poolId, out var stack) && stack.TryPop(out var recycledPrefab))
        {
            recycledPrefab.transform.position = spawnPos;
            recycledPrefab.EnableMovement();
            recycledPrefab.SetFallSpeed(randomSpeed);
        }
        else
        {
            Instantiate(choice.gameObject, spawnPos, Quaternion.identity, _ediblesParent.transform);
        }
    }

    // Calculate the random spawn position factoring in the size of the provided sprite renderer
    private Vector2 RandomSpawnPosition(SpriteRenderer spriteRenderer)
    {
        // Calculate the X value making sure that any part of our sprite will not be out of the screen
        var randomX = Random.Range(_bottomLeftField.x, _topRightField.x);
        var localBounds = spriteRenderer.localBounds;
        var clampedX = Mathf.Clamp(randomX, _bottomLeftField.x + localBounds.extents.x, _topRightField.x - localBounds.extents.x);

        return new Vector2(clampedX, _topRightField.y + localBounds.size.y * 3);
    }

    // Used from the EventListener to be triggered when an object is out of bounds
    public void RecycleEdible(Edible edible)
    {
        if (_ediblePool.TryGetValue(edible.data.poolId, out var stack))
        {
            stack.Push(edible);
        }
        else
        {
            var newStack = new Stack<Edible>();
            newStack.Push(edible);
            _ediblePool[edible.data.poolId] = newStack;
        }
        
        edible.DisableMovement();
        edible.transform.position = _outsideField;
    }

    // Used from the EventListener to do cleanup when the game finishes
    public void GameOver(int _)
    {
        DestroyImmediate(_ediblesParent);
        _ediblesParent = new GameObject("Edibles");
        // Remember to cleanup the pool as well
        _ediblePool.Clear();
    }
}
