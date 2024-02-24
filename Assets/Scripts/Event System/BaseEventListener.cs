using UnityEngine;
using UnityEngine.Events;

public abstract class EventListener<T>: MonoBehaviour
{
    public GameEvent<T> gameEvent;
    public UnityEvent<T> eventTriggered;

    private void OnEnable()
    {
        gameEvent.AddListener(this);
    }

    private void OnDisable()
    {
        gameEvent.RemoveListener(this);
    }

    public void OnEventTriggered(T objectType)
    {
        eventTriggered?.Invoke(objectType);
    }
}
