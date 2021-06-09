using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public ChaseState chaseState;
    public AttackState attackState;
    public Transform archer;
    public string animatorStringForIdle;


    StateModel archerEntity;


    private string stateString = "idleState";
    public void Start()
    {
        archerEntity = archer.GetComponent<StateModel>();
        runFixedUpdate = false;
    }

    public override State RunCurrentState()
    {

        FlipCharPeriodically();

        if (archerEntity.IsChasing && archerEntity.returnToSpawnPoint())
        {
            return chaseState;
        }


        State nextState = checkPlayerInRange();

        archerEntity.Animator.Play(animatorStringForIdle);


        // only increase time if enemy isn't in attacking
        if (archerEntity.TimeSinceLastShot >= archerEntity.ShootingCooldown && archerEntity.transform.position.x != archerEntity.SpawnPosition.x && archerEntity.TimeSinceAwayFromSpawn < archerEntity.TimePassUntilMoveBack)
        {
            archerEntity.TimeSinceAwayFromSpawn += Time.deltaTime;
        }


        return nextState;
    }

    public void FlipCharPeriodically()
    {
        if (archerEntity.TimeSinceLastFlip >= archerEntity.TimePassFlipChar)
        {

            //Flip char

            if (archerEntity.FacingRight)
            {
                archerEntity.FacingRight = false;
                archerEntity.transform.eulerAngles = new Vector3(0, 180, 0);

            }
            else
            {
                archerEntity.FacingRight = true;
                archerEntity.transform.eulerAngles = new Vector3(0, 0, 0);

            }

            archerEntity.TimeSinceLastFlip = 0;
        }
    }


    public State checkPlayerInRange()
    {
        float distanceFromSpawnPoint = Mathf.Abs(archerEntity.transform.position.x - archerEntity.SpawnPosition.x);

        if (archerEntity.PlayerInSight)
        {
            
            if (Mathf.Abs(archerEntity.Player.position.x - archerEntity.transform.position.x) <= archerEntity.AttackDistance)
            {
                if (archerEntity.TimeSinceLastShot >= archerEntity.ShootingCooldown)
                {
                    return attackState;
                }
            }
            else
            {
                if (distanceFromSpawnPoint < archerEntity.MaxDistanceFromSpawnPoint)
                {
                    return chaseState;
                }

            }
        }
        else if (Mathf.Abs(archerEntity.transform.position.x - archerEntity.SpawnPosition.x) > 0.1f)
        {
            return chaseState;
        }

        return this;
    }

    public override string getStateType()
    {
        return stateString;
    }

}
