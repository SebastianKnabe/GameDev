using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public ChaseState chaseState;
    public float sightRange = 5.0f;

    Transform states;
    Transform archer;
    ArcherFieldOfViewScript fov;

    private Animator idleAnimation;

    public void Start()
    {
        states = transform.parent;
        archer = states.transform.parent;
        fov = archer.transform.GetComponentInChildren<ArcherFieldOfViewScript>();
        idleAnimation = archer.GetComponent<Animator>();
    }

    public override State RunCurrentState()
    {
        if (fov.getPlayerInSight())
        {
            return chaseState;
        }
        Debug.Log("play idle");
        idleAnimation.Play("archer_enemy_idle");
        return this;
    }
}
