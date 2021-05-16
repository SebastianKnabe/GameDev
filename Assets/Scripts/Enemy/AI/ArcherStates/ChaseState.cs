using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public IdleState idleState;
    public AttackState attackState;
    public float criticalDistance; 


    Transform states;
    Transform archer;
    ArcherFieldOfViewScript fov;

    private Animator runAnimation;

    public void Start()
    {
        states = transform.parent;
        archer = states.transform.parent;
        fov = archer.transform.GetComponentInChildren<ArcherFieldOfViewScript>();
        runAnimation = archer.GetComponent<Animator>();
    }

    public override State RunCurrentState()
    {

        runAnimation.Play("archer_enemy_run");

        if (!fov.getPlayerInSight())
        {
            return idleState;
        }
        else if(fov.getPlayerInSight() && Mathf.Abs(fov.getPlayerTransform().position.x - transform.position.x) <= criticalDistance )
        {
            return attackState;
        }

        return this;
    }
}

