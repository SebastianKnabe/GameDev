using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public IdleState idleState;
    public ChaseState chaseState;

    Transform states;
    Transform archer;
    ArcherFieldOfViewScript fov;

    private Animator attackAnimation;


    public void Start()
    {
        states = transform.parent;
        archer = states.transform.parent;
        fov = archer.transform.GetComponentInChildren<ArcherFieldOfViewScript>();
        attackAnimation = archer.GetComponent<Animator>();
    }

    public override State RunCurrentState()
    {

        if (fov.getPlayerInRange() && !fov.getPlayerInSight())
        {
            return chaseState;
        }else if (!fov.getPlayerInSight())
        {
            return idleState;
        }else if(fov.getPlayerInRange() && fov.getPlayerInSight())
        {
            //attack
            attackAnimation.Play("archer_enemy_shoot");
            
        }
        return this;
    }
}
