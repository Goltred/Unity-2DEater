using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EdibleSpawner : MonoBehaviour
{
    public List<SpriteRenderer> EdiblePrefabs = new List<SpriteRenderer>();

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
        var randomIndex = Mathf.Clamp(Random.Range(0, ediblesUpperLimit), 0, ediblesUpperLimit);
        var choice = EdiblePrefabs.Skip(randomIndex).First();

        var spawnPos = RandomSpawnPosition(choice);
        Instantiate(choice.gameObject, spawnPos, Quaternion.identity);
        //TODO: Implement pooling
    }

    // Calculate the random spawn position factoring in the size of the provided sprite renderer
    private Vector2 RandomSpawnPosition(SpriteRenderer renderer)
    {
        // Calculate the X value making sure that any part of our sprite will not be out of the screen
        var randomX = Random.Range(bottomLeftField.x, topRightField.x);
        var localBounds = renderer.localBounds;
        var clampedX = Mathf.Clamp(randomX, bottomLeftField.x + localBounds.extents.x, topRightField.x - localBounds.extents.x);

        return new Vector2(clampedX, topRightField.y + localBounds.size.y * 3);
    }
}
