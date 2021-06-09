using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ChaseState : State
{

    public Transform archer;
    public IdleState idleState;
    public AttackState attackState;
    public JumpState jumpState;



    public string animatorStringForRun;
    public string animatorStringForFall;

    //public float jumpingSpeed = 10f;
    public bool jumpEnabled = true;
    public bool directionLookEnabled = true;


    private StateModel archerEntity;
    private string stateString = "chaseState";
    private int wallLayerMask;

    
    public void Start()
    {

        archerEntity = archer.GetComponentInChildren<StateModel>();
        wallLayerMask = LayerMask.GetMask("JumpingWall");
        runFixedUpdate = true;

    }


    //@TODO attack cooldown is too low when the enemy is heading back to the spawn point
    public override State RunCurrentState()
    {

        if (!archerEntity.IsChasing)
        {
            archerEntity.IsChasing = true;
            archerEntity.TimeSinceAwayFromSpawn = 0;
        }

        if(archerEntity.returnToSpawnPoint() && Mathf.Abs(archerEntity.transform.position.x - archerEntity.SpawnPosition.x) <= 0.1f)
        {
            
            archerEntity.IsChasing = false;
            return idleState;
        }
     
     
        // if enemy has passed the max distance 
        if ( archerEntity.TimeSinceAwayFromSpawn < archerEntity.TimePassUntilMoveBack && Mathf.Abs(archerEntity.transform.position.x - archerEntity.SpawnPosition.x) > archerEntity.MaxDistanceFromSpawnPoint)
        {
            return idleState;
          
        }

        archerEntity.Flip();


        /**
        if ((archerEntity.FacingRight && !archerEntity.PlayerInSight && archer.transform.position.x >= archerEntity.LastPlayerPosition.x)
            || (!archerEntity.FacingRight && !archerEntity.PlayerInSight && archer.transform.position.x <= archerEntity.LastPlayerPosition.x))
        {
            return idleState;
        }
        else
        */

        if (archerEntity.PlayerInSight && Mathf.Abs(archerEntity.Player.position.x - archerEntity.transform.position.x) <= archerEntity.AttackDistance)
        {
           
            if (archerEntity.TimeSinceLastShot >= archerEntity.ShootingCooldown)
            {
                return attackState;
                
            }
            return idleState;
        }

        return chasePlayer();
        //chase player
        //archer.position = Vector3.MoveTowards(archer.position, new Vector3(archerEntity.getPlayerTransform().position.x, 0, 0), archerEntity.runningSpeed * Time.deltaTime);



    }

    private State chasePlayer()
    {

        Collider2D hitInfo = Physics2D.OverlapCircle(archerEntity.WallCheck.position, archerEntity.CircleRadius, wallLayerMask);
        archerEntity.CheckingWall = Physics2D.OverlapCircle(archerEntity.WallCheck.position, archerEntity.CircleRadius, wallLayerMask);
        archerEntity.IsGrounded = Physics2D.OverlapBox(archerEntity.GroundCheck.position, archerEntity.BoxSize, 0, archerEntity.LayerMask);
        
    

        if (archerEntity.CheckingWall && jumpEnabled)
        {

           
            Vector3 offsetVector = hitInfo.transform.position;
            Vector3 parentWallColliderVector = hitInfo.transform.parent.transform.position;
            Vector3 relativePositionOfDetectedWall = hitInfo.transform.parent.transform.InverseTransformPoint(offsetVector);
            Vector3 topPositionOfDetectedWall = parentWallColliderVector + relativePositionOfDetectedWall;

            Debug.Log(topPositionOfDetectedWall);

            archerEntity.MiddlePoint = new Vector3((topPositionOfDetectedWall.x), topPositionOfDetectedWall.y, 0);

            return jumpState;


        }

        
        archer.Translate(archerEntity.RunningSpeed * Time.deltaTime, 0f, 0f);
        
        if (!archerEntity.IsGrounded)
        {
            archerEntity.Animator.Play(animatorStringForFall);
        }
        else
        {
            archerEntity.Animator.Play(animatorStringForRun);
        }
        
        return this;

    }

    /*
    private bool jumping = false;
    private float airTime = 0.5f;
    private float jumpDist = 20.0f;

    IEnumerator jump()
    {
        Debug.Log("jump coroutine started");
        archer.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        float timer = 0;
        jumping = true;
        while (jumping && timer < airTime)
        {
            float percentJumpCompleted = timer / airTime;

            Vector2 jumpVectorThisFrame = Vector2.Lerp((Vector2.up + new Vector2(0, jumpDist)), Vector2.zero, percentJumpCompleted);
            archer.GetComponent<Rigidbody2D>().AddForce(jumpVectorThisFrame);
            timer += Time.deltaTime;
            yield return null;
        }

        jumping = false;
        Debug.Log("jump coroutine ended");
    }
    **/
    private Vector3 CalcQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;
        return p;
    }

    

    public override string getStateType()
    {
        return stateString;
    }

 

   

}

