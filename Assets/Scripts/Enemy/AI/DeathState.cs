using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{


    public Transform archer;
    public string animatorDeathString;

    private StateModel archerEntity;
    private string stateString = "deathState";

    void Start()
    {
        archerEntity = archer.GetComponent<StateModel>();
        initVariables();
    }

    public override string getStateType()
    {
        return stateString;
    }

    public override void initVariables()
    {
       
    }

    public override State RunCurrentState()
    {
        archerEntity.Animator.Play(animatorDeathString);

        if (archerEntity.Animator.GetCurrentAnimatorStateInfo(0).IsName(animatorDeathString) &&
               archerEntity.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {

            Destroy(archer.gameObject);

            //return idleState;
        }

        return this;
    }
}
