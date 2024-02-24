using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class OutOfBoundsNotifier : MonoBehaviour
{
    public GameEventEdible onOutOfBounds;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Edible>(out var edible))
        {
            onOutOfBounds?.Trigger(edible);
            return;
        }
        
        // Make sure to destroy those objects that we don't know about to avoid
        // wasting resources
        Destroy(other.gameObject);
    }
}
