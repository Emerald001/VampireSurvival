using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> {
    public T Owner { get; protected set; }
    public State<T> CurrentState { get; protected set; }

    private Dictionary<System.Type, State<T>> StateDic = new();

    public StateMachine(T owner) {
        Owner = owner;
    }

    public void OnUpdate() {
        CurrentState?.OnUpdate();
    }

    public void ChangeState(System.Type state) {
        if (CurrentState == StateDic[state]) {
            Debug.LogError("Can't go into state it's already in");
            return;
        }

        CurrentState?.OnExit();

        if (StateDic.ContainsKey(state)) {
            var tmpState = StateDic[state];
            tmpState.OnEnter();
            CurrentState = tmpState;
        }
    }

    public void AddState(System.Type type, State<T> state) {
        if (StateDic.ContainsValue(state)) {
            Debug.LogError("Already added State");
            return;
        }
        StateDic.Add(type, state);
    }

    public void RemoveState(System.Type type) {
        if (!StateDic.ContainsKey(type)) {
            Debug.LogError("State not in the list");
            return;
        }
        StateDic.Remove(type);
    }
}