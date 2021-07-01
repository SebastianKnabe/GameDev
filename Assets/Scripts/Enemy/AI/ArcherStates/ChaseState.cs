using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ChaseState : State
{

    public Transform archer;
    public IdleState idleState;
    public BlockState blockState;
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


    public override void initVariables()
    {

    }

    public void Start()
    {

        archerEntity = archer.GetComponentInChildren<StateModel>();
        wallLayerMask = LayerMask.GetMask("JumpingWall");
        initVariables();

    }


    public override State RunCurrentState()
    {


        if (archerEntity.BlockProjectile())
        {
            return blockState;
        }

        if (archerEntity.returnToSpawnPoint() && Mathf.Abs(archerEntity.transform.position.x - archerEntity.SpawnPosition.x) <= 0.1f)
        {
            
            archerEntity.IsChasing = false;
            return idleState;
        }
     
     
        // if enemy has passed the max distance 
        if (archerEntity.TimeSinceAwayFromSpawn < archerEntity.timePassUntilMoveBack && Mathf.Abs(archerEntity.transform.position.x - archerEntity.SpawnPosition.x) > archerEntity.maxDistanceFromSpawnPoint)
        {
            return idleState;
          
        }

        /*
        // also refresh timer cooldown if player is seen while the enemy is running back to the spawn point
        if (archerEntity.returnToSpawnPoint() && archerEntity.PlayerInSight && archerEntity.TimeSinceLastRetargeting >= archerEntity.TimePassUntilRetargeting)
        {
            archerEntity.TimeSinceAwayFromSpawn = 0f;
            archerEntity.TimeSinceLastRetargeting = 0f;
        }
        */
        archerEntity.Flip();




        if (archerEntity.playerInSight && Mathf.Abs(archerEntity.Player.position.x - archerEntity.transform.position.x) <= archerEntity.attackDistance)
        {
           
            if (archerEntity.TimeSinceLastShot >= archerEntity.shootingCooldown)
            {
                return attackState;
                
            }
            return idleState;
        }

        return chasePlayer();
        

    }

    private State chasePlayer()
    {
        //  wallcheck 0, groundcheck 1, heightcheck 2
        /*
        Collider2D hitInfo = Physics2D.OverlapBox(archerEntity.wallCheck.position, archerEntity.wallBoxSize, 0, wallLayerMask);
        archerEntity.CheckingWall = Physics2D.OverlapBox(archerEntity.wallCheck.position, archerEntity.wallBoxSize,0, wallLayerMask);
        archerEntity.IsGrounded = Physics2D.OverlapBox(archerEntity.groundCheck.position, archerEntity.groundBoxSize, 0, archerEntity.LayerMask);
        bool detectedGround = Physics2D.Raycast(archerEntity.heightCheck.position,Vector2.down, archerEntity.maxFallHeight, archerEntity.LayerMask);
        */

        Collider2D hitInfo = Physics2D.OverlapBox(archerEntity.colliders[0].transform.position, archerEntity.colliders[0].boxSize, 0, wallLayerMask);
        archerEntity.CheckingWall = Physics2D.OverlapBox(archerEntity.colliders[0].transform.position, archerEntity.colliders[0].boxSize, 0, wallLayerMask);
        archerEntity.IsGrounded = Physics2D.OverlapBox(archerEntity.colliders[1].transform.position, archerEntity.colliders[1].boxSize, 0, archerEntity.LayerMask);
        bool detectedGround = Physics2D.Raycast(archerEntity.colliders[2].transform.position,Vector2.down, archerEntity.colliders[2].boxSize.y, archerEntity.LayerMask);

        

        if (archerEntity.CheckingWall && jumpEnabled)
        {

            
            if (hitInfo.bounds.size.y > archerEntity.maxFallHeight)
            {
                return idleState;
            }

            Vector3 offsetVector = hitInfo.transform.position;
            Vector3 parentWallColliderVector = hitInfo.transform.parent.transform.position;
            Vector3 relativePositionOfDetectedWall = hitInfo.transform.parent.transform.InverseTransformPoint(offsetVector);
            Vector3 topPositionOfDetectedWall = parentWallColliderVector + relativePositionOfDetectedWall;

            //Debug.Log(topPositionOfDetectedWall);

            archerEntity.MiddlePoint = new Vector3((topPositionOfDetectedWall.x), topPositionOfDetectedWall.y, 0);

            return jumpState;


        }


        if (!detectedGround)
        {
            return idleState;
        }
        
        archer.Translate(archerEntity.runningSpeed * Time.deltaTime, 0f, 0f);
        
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

