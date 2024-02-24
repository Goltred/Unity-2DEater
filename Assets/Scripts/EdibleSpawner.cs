using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EdibleSpawner : MonoBehaviour
{
    public List<Edible> EdiblePrefabs = new();
    private Dictionary<int, Stack<Edible>> prefabPool = new();
    
    private int ediblesUpperLimit;
    private Vector3 topRightField;
    private Vector3 bottomLeftField;

    void Start()
    {
        bottomLeftField = Camera.main.ViewportToWorldPoint(Vector3.zero);
        topRightField = Camera.main.ViewportToWorldPoint(Vector3.one);
        ediblesUpperLimit = EdiblePrefabs.Count - 1;
    }

    public void Spawn()
    {
        // In order to offer randomness we pick the next edible randomly
        var randomIndex = Mathf.Clamp(Random.Range(0, ediblesUpperLimit), 0, ediblesUpperLimit);
        var choice = EdiblePrefabs.Skip(randomIndex).First();

        var spawnPos = RandomSpawnPosition(choice.GetRenderer());

        // To reduce instancing we check if we have an available copy of the edible we want to spawn and use that instead
        if (prefabPool.TryGetValue(choice.Data.PoolId, out var stack) && stack.TryPop(out var recycledPrefab))
        {
            recycledPrefab.transform.position = spawnPos;
            recycledPrefab.EnableMovement();
        }
        else
        {
            Instantiate(choice.gameObject, spawnPos, Quaternion.identity);
        }
    }

    // Calculate the random spawn position factoring in the size of the provided sprite renderer
    private Vector2 RandomSpawnPosition(SpriteRenderer spriteRenderer)
    {
        // Calculate the X value making sure that any part of our sprite will not be out of the screen
        var randomX = Random.Range(bottomLeftField.x, topRightField.x);
        var localBounds = spriteRenderer.localBounds;
        var clampedX = Mathf.Clamp(randomX, bottomLeftField.x + localBounds.extents.x, topRightField.x - localBounds.extents.x);

        return new Vector2(clampedX, topRightField.y + localBounds.size.y * 3);
    }

    // Used from the EventListener to be triggered when an object is out of bounds
    public void RecycleEdible(GameObject edible)
    {
        if (!edible.TryGetComponent<Edible>(out var component))
        {
            // This is not the edible we are looking for
            return;
        }
        
        if (prefabPool.TryGetValue(component.Data.PoolId, out var stack))
        {
            stack.Push(component);
        }
        else
        {
            var newStack = new Stack<Edible>();
            newStack.Push(component);
            prefabPool[component.Data.PoolId] = newStack;
        }
        
        component.DisableMovement();
    }
}
