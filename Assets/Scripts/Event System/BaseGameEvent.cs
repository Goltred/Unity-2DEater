using System.Collections.Generic;
using UnityEngine;

// Base class for triggering events that will also send a specific object as an argument
public abstract class GameEvent<T> : ScriptableObject
{
    private List<EventListener<T>> _listeners = new List<EventListener<T>>();

    public void Trigger(T objectType)
    {
        foreach (var listener in _listeners)
        {
            listener.OnEventTriggered(objectType);
        }
    }

    public void AddListener(EventListener<T> listener)
    {
        _listeners.Add(listener);
    }

    public void RemoveListener(EventListener<T> listener)
    {
        _listeners.Remove(listener);
    }
}
