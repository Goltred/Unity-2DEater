using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class OutOfBoundsNotifier : MonoBehaviour
{
    public GameEventGameObject OnOutOfBounds;

    void OnTriggerEnter2D(Collider2D other)
    {
        OnOutOfBounds?.Trigger(other.gameObject);
    }
}
