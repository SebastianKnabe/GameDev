using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingState: State
{

    private string stateString = "patrollingState";
    public string animatorStringForPatrolling;
    public Transform sneaker;
    public JumpAttackState jumpAttackState;

    private StateModel sneakerEntity;
    private Rigidbody2D sneakerRigidbody;

    public override void initVariables()
    {
    }

    public void Start()
    {
        sneakerEntity = sneaker.GetComponent<StateModel>();
        sneakerRigidbody = sneakerEntity.GetComponent<Rigidbody2D>();
    }

    public override State RunCurrentState()
    {
        sneakerEntity.Animator.Play(animatorStringForPatrolling);
        updateDirection();
        //sneakerEntity.CheckingWall = Physics2D.OverlapBox(sneakerEntity.wallCheck.position, sneakerEntity.wallBoxSize, 0, sneakerEntity.LayerMask);
        //sneakerEntity.IsGrounded = Physics2D.OverlapBox(sneakerEntity.groundCheck.position, sneakerEntity.groundBoxSize, 0, sneakerEntity.LayerMask);


        bool checkWall = Physics2D.OverlapBox(sneakerEntity.colliders[0].transform.position, sneakerEntity.colliders[0].boxSize, 0, sneakerEntity.LayerMask);
        bool isGrounded = Physics2D.OverlapBox(sneakerEntity.colliders[1].transform.position, sneakerEntity.colliders[1].boxSize, 0, sneakerEntity.LayerMask);
        bool checkGround = Physics2D.OverlapBox(sneakerEntity.colliders[2].transform.position, sneakerEntity.colliders[2].boxSize, 0, sneakerEntity.LayerMask);
        
      
        

        if ( (checkWall || checkGround == false) && isGrounded )
        {
            sneakerEntity.FacingRight = !sneakerEntity.FacingRight;
        }

        int moveDirection = sneakerEntity.FacingRight ? 1 : -1;
        sneakerRigidbody.velocity = new Vector2(sneakerEntity.runningSpeed * moveDirection , sneakerRigidbody.velocity.y);

       


        return checkPlayerInRange();
    }


    public State checkPlayerInRange()
    {
       


        //Debug.Log("detectedAbyss " + detectedDeepAbyss);
        if (sneakerEntity.playerInSight)
        {

            if (Mathf.Abs(sneakerEntity.Player.position.x - sneakerEntity.transform.position.x) <= sneakerEntity.attackDistance)
            {
               return jumpAttackState;
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

    public void updateDirection()
    {


        if (sneakerEntity.FacingRight)
        {
            sneakerEntity.transform.eulerAngles = new Vector3(sneakerEntity.transform.eulerAngles.x, 0, sneakerEntity.transform.eulerAngles.z);
        }
        else
        {
            sneakerEntity.transform.eulerAngles = new Vector3(sneakerEntity.transform.eulerAngles.x, 180, sneakerEntity.transform.eulerAngles.z);
        }

    }

  
}
