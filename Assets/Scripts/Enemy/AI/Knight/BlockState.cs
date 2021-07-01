using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockState : State
{

    public Transform archer;
    public IdleState idleState;
    public string animatorStringForBlock;

    private StateModel archerEntity;
    private string stateString = "blockState";
    public override string getStateType()
    {
        return stateString;
    }

    public override State RunCurrentState()
    {
        archerEntity.BlockProjectile();
        archerEntity.Animator.Play(animatorStringForBlock);

        if (archerEntity.Animator.GetCurrentAnimatorStateInfo(0).IsName(animatorStringForBlock) &&
               archerEntity.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            // maybe reflect bullet?
            return idleState;

        }

        return this;
    }

    public override void initVariables()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        archerEntity = archer.GetComponent<StateModel>();
        initVariables();
    }

}
