using System;

public class Transition<T>
{
    public Transition(Predicate<T> condition, Type toState)
    {
        this.condition = condition;
        this.toState = toState;
    }

    public Predicate<T> condition;
    public Type toState;
}
