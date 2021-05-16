using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{

    public State currentState;

    // Update is called once per frame
    void Update()
    {
        RunStateMachine();
    }
    public void RunStateMachine()
    {
        State nextState = currentState?.RunCurrentState();
        if(nextState != null)
        {
            SwitchState(nextState);
        }
    }

    private void SwitchState(State nextState)
    {
        currentState = nextState;
    }
}
