using System.Collections.Generic;
using UnityEngine;

// Base class for triggering events that will also send a specific object as an argument
public abstract class GameEvent<T> : ScriptableObject
{
    private List<EventListener<T>> listeners = new List<EventListener<T>>();

    public void Trigger(T objectType)
    {
        foreach (var listener in listeners)
        {
            listener.OnEventTriggered(objectType);
        }
    }

    public void AddListener(EventListener<T> listener)
    {
        listeners.Add(listener);
    }

    public void RemoveListener(EventListener<T> listener)
    {
        listeners.Remove(listener);
    }
}
