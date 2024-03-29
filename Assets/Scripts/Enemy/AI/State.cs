﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public abstract State RunCurrentState();
    public abstract string getStateType();
    public abstract void initVariables();
    // runFixedUpdate = 0 means that the function will be executed in the FixedUpdate method.
    public bool runFixedUpdate;
}
