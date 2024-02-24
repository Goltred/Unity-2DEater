using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class OutOfBoundsNotifier : MonoBehaviour
{
    public GameEventGameObject onOutOfBounds;

    void OnTriggerEnter2D(Collider2D other)
    {
        onOutOfBounds?.Trigger(other.gameObject);
    }
}
