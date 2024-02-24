using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class OutOfBoundsNotifier : MonoBehaviour
{
    public GameEventGameObject OnOutOfBounds;

    private void OnTriggerEnter(Collider other)
    {
        OnOutOfBounds?.Trigger(other.gameObject);
    }
}
