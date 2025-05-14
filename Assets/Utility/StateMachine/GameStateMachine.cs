using UnityEngine;

public class GameStateMachine : MonoBehaviour {
    private GameState currentState = null;

    public void OnUpdate() {
        if (currentState != null)
            return;

        currentState.OnUpdate();
    }

    public void NextState(GameState newState) {
        currentState?.OnExit();

        currentState = newState;
        currentState?.OnEnter();
    }

    public void Stop() {
        currentState = null;
    }
}
