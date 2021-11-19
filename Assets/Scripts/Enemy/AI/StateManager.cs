using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{

    public State currentState;

    // Update is called once per frame
    void Update()
    {
        if (!currentState.runFixedUpdate)
        {
            RunStateMachine();
        }
    }

    void FixedUpdate()
    {
        if (currentState.runFixedUpdate)
        {
            RunStateMachine();
        }
    }
    public void RunStateMachine()
    {
        State nextState = currentState?.RunCurrentState();
        if (nextState != null)
        {
            // init variables if the next state is different from the previous one
            State previousState = currentState;
            SwitchState(nextState);
            if (nextState.getStateType() != previousState.getStateType())
            {
                currentState.initVariables();
            }
        }
    }

    private void SwitchState(State nextState)
    {
        currentState = nextState;
    }
}
