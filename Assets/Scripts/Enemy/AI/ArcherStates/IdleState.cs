using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public ChaseState chaseState;
    public AttackState attackState;
    public BlockState blockState;
    public Transform archer;
    public string animatorStringForIdle;


    StateModel archerEntity;


    private string stateString = "idleState";

    public override void initVariables()
    {

    }
    public void Start()
    {
        archerEntity = archer.GetComponent<StateModel>();
        initVariables();
    }

    public override State RunCurrentState()
    {
        FlipCharPeriodically();

        if (archerEntity.BlockProjectile()) {
            return blockState;
        }

        if (archerEntity.returnToSpawnPoint())
        {
            return chaseState;
        }


        State nextState = checkPlayerInRange();

        archerEntity.Animator.Play(animatorStringForIdle);

        updateChaseTimer();

       

        return nextState;
    }

   
    public void updateChaseTimer()
    {
        // only increase time if enemy isn't attacking
        if (archerEntity.TimeSinceLastShot >= archerEntity.shootingCooldown && archerEntity.transform.position.x != archerEntity.SpawnPosition.x && archerEntity.TimeSinceAwayFromSpawn < archerEntity.timePassUntilMoveBack)
        {
            archerEntity.TimeSinceAwayFromSpawn += Time.deltaTime;
        }
    }


    public void FlipCharPeriodically()
    {
        if (archerEntity.TimeSinceLastFlip >= archerEntity.timePassFlipChar && !archerEntity.IsChasing)
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
        //bool detectedGround = Physics2D.Raycast(archerEntity.colliders[2].transform.position, Vector2.down, archerEntity.maxFallHeight, archerEntity.LayerMask);
        bool detectedGround = Physics2D.Raycast(archerEntity.colliders[2].transform.position, Vector2.down, archerEntity.colliders[2].boxSize.y, archerEntity.LayerMask);

       

        //Debug.Log("detectedAbyss " + detectedDeepAbyss);
        if (archerEntity.playerInSight)
        {
            
            if (Mathf.Abs(archerEntity.Player.position.x - archerEntity.transform.position.x) <= archerEntity.attackDistance)
            {
                if (archerEntity.TimeSinceLastShot >= archerEntity.shootingCooldown)
                {
                    return attackState;
                }
            }
            else if (!detectedGround)
            {
                return this;
            }
            else if (distanceFromSpawnPoint < archerEntity.maxDistanceFromSpawnPoint)
            {
                return chaseState;
            }
        }
        /*
        else if (Mathf.Abs(archerEntity.transform.position.x - archerEntity.SpawnPosition.x) > 0.1f)
        {
            return chaseState;
        }
        /*/
        return this;
    }

    public override string getStateType()
    {
        return stateString;
    }

   
}
