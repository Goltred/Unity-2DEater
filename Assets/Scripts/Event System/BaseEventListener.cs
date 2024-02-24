using UnityEngine;
using UnityEngine.Events;

public abstract class EventListener<T>: MonoBehaviour
{
    public GameEvent<T> GameEvent;
    public UnityEvent<T> eventTriggered;

    private void OnEnable()
    {
        GameEvent.AddListener(this);
    }

    private void OnDisable()
    {
        GameEvent.RemoveListener(this);
    }

    public void OnEventTriggered(T objectType)
    {
        eventTriggered?.Invoke(objectType);
    }
}
