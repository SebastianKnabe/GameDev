using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : State
{
    public ChaseState chaseState;
    public IdleState idleState;

    Vector3 middlePoint;

    public Transform archer;
    public float FallingThreshold = -8;
    StateModel archerEntity;

    private Animator jumpAnimator;
    private string stateString = "jumpState";

    private bool inJump = false;
    public bool fall = false;
    public bool move = false;

    private bool started = false;
    private bool playerFelt = false;
    private Vector3 playerPosition;
    private Vector3 archerPosition;



    // Start is called before the first frame update
    public void Start()
    {

        

     
        archerEntity = archer.GetComponent<StateModel>();
        jumpAnimator = archer.GetComponent<Animator>();
        middlePoint = archerEntity.middlePoint;

    }

    public override State RunCurrentState()
    {
        archerEntity.checkingWall = Physics2D.OverlapCircle(archerEntity.wallCheck.position, archerEntity.circleRadius, archerEntity.getLayerMask());
        archerEntity.isGrounded = Physics2D.OverlapBox(archerEntity.groundCheck.position, archerEntity.boxSize, 0, archerEntity.getLayerMask());

        //Debug.Log(archer.GetComponent<Rigidbody2D>().velocity.y);
        /*
         * 
         * check if jump is finished and changes state then
         * 
        float current_y = path.vectorPath[currentWaypoint].y;
        int index = 0;
        for (int i = currentWaypoint; i < path.vectorPath.Count; i++)
        {
            if (current_y >= path.vectorPath[i].y)
            {
                index = i;
                break;
            }
            if (current_y < path.vectorPath[i].y)
            {
                current_y = path.vectorPath[i].y;
            }
        }
        //StartCoroutine(jump());
        
        /*/



        //interpolateAmount = (interpolateAmount + Time.deltaTime) % 1f;

        if(started && playerFelt && archerEntity.isGrounded)
        {
            playerFelt = false;
            started = false;
            return idleState;
        }

        if (!started)
        {
            archerPosition = archer.position;
            started = true;
        }



        if(archer.GetComponent<Rigidbody2D>().velocity.y < FallingThreshold)
        {
            jumpAnimator.Play("archer_enemy_fall");
            playerFelt = true;
        }
        else
        {
            jumpAnimator.Play("archer_enemy_jump");
        }


       

       


       Vector3 pointA = archer.position;
       Vector3 pointB = new Vector3(archer.position.x, archerEntity.middlePoint.y + 20, 0);
       Vector3 pointC = new Vector3(archerEntity.middlePoint.x, archerEntity.middlePoint.y + 20, 0);
       Vector3 pointD = new Vector3(archerEntity.middlePoint.x, archerEntity.middlePoint.y, 0);




       Vector3 result = GetPointOnBezierCurve(pointA, pointB, pointC, pointD, Time.deltaTime * 15);
       inJump = true;

       archer.GetComponent<Rigidbody2D>().position = result;
        /*
       * 
       * 
      Vector3 pointA = archer.position;
      Vector3 pointB = new Vector3(archer.position.x, playerPosition.y + 10, 0);
      Vector3 pointC = new Vector3(playerPosition.x, playerPosition.y + 10, 0);
      Vector3 pointD = new Vector3(playerPosition.x, playerPosition.y, 0);
       * 
     if(archerEntity.isGrounded && !inJump){


         float xDistance = archerEntity.middlePoint.x - archerEntity.transform.position.x;
         float yDistance = archerEntity.middlePoint.y - archerEntity.transform.position.y;

         archer.GetComponent<Rigidbody2D>().AddForce(new Vector2(xDistance, 20), ForceMode2D.Impulse);

         inJump = true;
     }

     */

        // transform.position = Vector3.Lerp(obj1, obj2, Time.deltaTime / finalSpeed);


        //pointAB = Vector3.Lerp(archer.position, middlePoint, Time.deltaTime);
        //pointBC = Vector3.Lerp(middlePoint, archerEntity.getPlayerTransform().position, Time.deltaTime);
        // Vector3 final = Vector3.Lerp(pointAB, pointBC, Time.deltaTime);
        //Vector3 final = Vector3.Lerp(archer.position, archerEntity.getPlayerTransform().position, Time.deltaTime);
        // Debug.Log("pointAB " + pointAB + "pointBC " + pointBC + "result " + final);
        //archer.GetComponent<Rigidbody2D>().position = Vector3.Lerp(archer.position, result, Time.deltaTime); ;
        //



        return this;

    }


    public override string getStateType()
    {
        return stateString;
    }

    Vector3 GetPointOnBezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        Vector3 velocity = Vector3.zero;
        Vector3 a = Vector3.SmoothDamp(p0, p1, ref velocity, t);
        Vector3 b = Vector3.SmoothDamp(p1, p2, ref velocity,t);
        Vector3 c = Vector3.SmoothDamp(p2, p3, ref velocity,t);
        Vector3 d = Vector3.SmoothDamp(a, b, ref velocity, t);
        Vector3 e = Vector3.SmoothDamp(b, c, ref velocity, t);
        Vector3 pointOnCurve = Vector3.SmoothDamp(d, e, ref velocity, t);

        /*
                float u = 1f - t;
                float t2 = t * t;
                float u2 = u * u;
                float u3 = u2 * u;
                float t3 = t2 * t;

                Vector3 result =
                   (u3) * p0 +
                   (3f * u2 * t) * p1 +
                   (3f * u * t2) * p2 +
                   (t3) * p3;

                return result;
                /**/

        return pointOnCurve;


    }



}

