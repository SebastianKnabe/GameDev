using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitState : State
{

    public Transform archer;
    public IdleState idleState;
    public DeathState deathState;
    public string animatorStringOnHit;
    private StateModel archerEntity;
    private EnemyEntity enemyEntity;
    private string stateString = "onHitState";

    public override string getStateType()
    {
        return stateString;
    }

    public override void initVariables()
    {
        
    }
    void Start()
    {
        archerEntity = archer.GetComponent<StateModel>();
        enemyEntity = archer.GetComponent<EnemyEntity>();
        initVariables();
    }

    public override State RunCurrentState()
    {

        archerEntity.Animator.Play(animatorStringOnHit);

        if (archerEntity.Animator.GetCurrentAnimatorStateInfo(0).IsName(animatorStringOnHit) &&
               archerEntity.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            if(archerEntity.GetComponent<EnemyEntity>().getCurrentHitPoints() <= 0)
            {
                return deathState;
            }
            return idleState;
        }

        return this;
    }

   

 
}
