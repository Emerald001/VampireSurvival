using System.Collections.Generic;

public abstract class State<T>
{
    public StateMachine<T> StateMachine { get; protected set; }
    public List<Transition<T>> Transitions = new();

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();

    public State (StateMachine<T> owner) {
        this.StateMachine = owner;
    }

    public virtual void AddTransition(Transition<T> transition) {
        Transitions.Add(transition);
    }
}