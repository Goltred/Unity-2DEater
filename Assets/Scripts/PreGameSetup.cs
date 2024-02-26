using UnityEngine;

/// <summary>
/// Simple class that triggers a GameEventEmpty when the object is spawned
/// </summary>
public class PreGameSetup : MonoBehaviour
{
    public GameEventEmpty onStartEvent;

    void Start()
    {
        onStartEvent?.Trigger(null);
    }
}
