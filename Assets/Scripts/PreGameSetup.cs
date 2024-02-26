using UnityEngine;

public class PreGameSetup : MonoBehaviour
{
    public GameEventEmpty onStartEvent;

    void Start()
    {
        onStartEvent?.Trigger(null);
    }
}
