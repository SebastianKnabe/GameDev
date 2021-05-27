using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public ChaseState chaseState;
    public AttackState attackState;
    //public float sightRange = 5.0f;
    //public float attackRange; 
    public Transform archer;
    Transform states;

    StateModel archerEntity;

    private Animator idleAnimation;
    private string stateString = "idleState";
    public void Start()
    {
        archerEntity = archer.GetComponent<StateModel>();
        idleAnimation = archer.GetComponent<Animator>();
    }

    public override State RunCurrentState()
    {
        if (archerEntity.getPlayerInSight())
        {
            if(Mathf.Abs(archerEntity.getPlayerTransform().position.x - archer.position.x) <= archerEntity.attackDistance )
            {
                if (archerEntity.timeSinceLastShot > archerEntity.shootingCooldown)
                {
                    return attackState;
                }
            }
            else
            {
                return chaseState;
            }
        }
        idleAnimation.Play("archer_enemy_idle");
        archerEntity.timeSinceLastShot += Time.fixedDeltaTime;
        return this;
    }



    public override string getStateType()
    {
        return stateString;
    }

}
