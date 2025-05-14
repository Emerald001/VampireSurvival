using System.Collections.Generic;

public static class EventManager<Enum> {
    private static readonly Dictionary<Enum, System.Action> eventActions = new();

    public static void Subscribe(Enum eventType, System.Action eventToSubscribe) {
        if (!eventActions.ContainsKey(eventType))
            eventActions.Add(eventType, null);

        eventActions[eventType] += eventToSubscribe;
    }

    public static void Unsubscribe(Enum eventType, System.Action eventToUnsubscribe) {
        if (eventActions.ContainsKey(eventType)) 
            eventActions[eventType] -= eventToUnsubscribe;
    }

    public static void Invoke(Enum eventType) {
        if (eventActions.ContainsKey(eventType))
            eventActions[eventType]?.Invoke();
    }
}

public static class EventManager<Enum, T> {
    private static readonly Dictionary<Enum, System.Action<T>> eventActions = new();

    public static void Subscribe(Enum eventType, System.Action<T> eventToSubscribe) {
        if (!eventActions.ContainsKey(eventType))
            eventActions.Add(eventType, null);

        eventActions[eventType] += eventToSubscribe;
    }

    public static void Unsubscribe(Enum eventType, System.Action<T> eventToUnsubscribe) {
        if (eventActions.ContainsKey(eventType)) 
            eventActions[eventType] -= eventToUnsubscribe;
    }

    public static void Invoke(Enum eventType, T obj) {
        if (eventActions.ContainsKey(eventType))
            eventActions[eventType]?.Invoke(obj);
    }
}