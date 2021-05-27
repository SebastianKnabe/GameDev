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


    public float jumpingSpeed = 10f;
    public bool jumpEnabled = true;
    public bool directionLookEnabled = true;


    private int currentWaypoint = 0;

    private bool jumping = false;
    private float airTime = 0.5f;
    private float jumpDist = 20.0f;
    private float interpolateAmount; 

    private bool runOnce = true;

    private StateModel archerEntity;

    Vector3 pointB;

    private Animator runAnimation;
    private string stateString = "chaseState";

    private int wallLayerMask;
    
    public void Start()
    {
        

        interpolateAmount = 0;
        archerEntity = archer.GetComponentInChildren<StateModel>();
        runAnimation = archer.GetComponent<Animator>();
        wallLayerMask = LayerMask.GetMask("JumpingWall");


    }

    public override State RunCurrentState()
    {

        //Debug.Log(Mathf.Abs(archerEntity.getPlayerTransform().position.x - archer.position.x));

        runAnimation.Play("archer_enemy_run");

        if (!archerEntity.getPlayerInSight())
        {
            return idleState;
        }
        else if(archerEntity.getPlayerInSight() && Mathf.Abs(archerEntity.getPlayerTransform().position.x - archer.position.x) <= archerEntity.attackDistance)
        {
            if (archerEntity.timeSinceLastShot > archerEntity.shootingCooldown)
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

        Collider2D hitInfo = Physics2D.OverlapCircle(archerEntity.wallCheck.position, archerEntity.circleRadius, wallLayerMask);
        archerEntity.checkingWall = Physics2D.OverlapCircle(archerEntity.wallCheck.position, archerEntity.circleRadius, wallLayerMask);
        archerEntity.isGrounded = Physics2D.OverlapBox(archerEntity.groundCheck.position, archerEntity.boxSize, 0, archerEntity.getLayerMask());
        


        // check if colliding with anything
       // Vector2 direction = ((Vector2) (path.vectorPath[currentWaypoint] - archer.position)).normalized;
        //Vector2 force = direction * archerEntity.runningSpeed * Time.deltaTime;
        //Vector2 force = direction * archerEntity.runningSpeed * Time.deltaTime;
        //float distance = Vector2.Distance(archer.transform.position, path.vectorPath[currentWaypoint]);

    

        if (archerEntity.checkingWall && jumpEnabled)
        {
            if (true)
            {
                //Debug.Log("wall");


                //archer.GetComponent<Rigidbody2D>().AddForce(Vector2.up * archerEntity.runningSpeed * Time.deltaTime);
                //archer.GetComponent<Rigidbody2D>().MovePosition((Vector2)archer.position + (direction * Vector2.up * jumpModifier * Time.deltaTime));
                //force *= Vector2.up * jumpModifier;


                /*
                int index = 0;
                bool checkJumpingSpot = false;
                for (int i = 0; i < path.vectorPath.Count - 1; i++)
                {
                    if (!checkJumpingSpot && path.vectorPath[i].x >= archer.GetComponent<Rigidbody2D>().transform.position.x && path.vectorPath[i + 1].y > path.vectorPath[i].y)
                    {
                        checkJumpingSpot = true;
                    }

                    if (checkJumpingSpot && path.vectorPath[i + 1].y <= path.vectorPath[i].y)
                    {
                        index = i + 1;
                        break;
                    }

                }
                /*/

                //archerEntity.middlePoint = new Vector3(archer.position.x + (path.vectorPath[index].x - archer.position.x)/2, path.vectorPath[index].y, 0);

                Debug.Log(hitInfo.bounds.center + " " + hitInfo);
               

                archerEntity.middlePoint = new Vector3((hitInfo.bounds.center.x), hitInfo.bounds.center.y, 0);

                //Debug.Log("middlePoint " + archerEntity.middlePoint);

                return jumpState;





                //Vector3 goalPos = CalcQuadraticBezierPoint(Time.deltaTime, archer.position, pointB, archerEntity.getPlayerTransform().position);

                //archer.GetComponent<Rigidbody2D>().position = Vector3.Lerp(archer.position, goalPos, Time.deltaTime);

                // float distance2 = Vector2.Distance(archer.transform.position, path.vectorPath[index]);
                //Debug.Log(archer.position+ "  " + path.vectorPath[index] + "  distance" + distance2);
                //archer.GetComponent<Rigidbody2D>().AddForce(new Vector2(distance2, jumpingSpeed), ForceMode2D.Impulse);

                //archer.GetComponent<Rigidbody2D>().position = Vector3.Lerp(archer.position, path.vectorPath[index], Time.deltaTime);
                //archer.GetComponent<Rigidbody2D>().velocity += new Vector3(Delta_X, 0, Delta_Y) * MovementSpeed * Time.deltaTime;
                //archer.GetComponent<Rigidbody2D>().AddForce(new Vector2(d, jumpingSpeed), ForceMode2D.Impulse);
                //archer.position = Vector3.MoveTowards(archer.position, new Vector3(archerEntity.getPlayerTransform().position.x, 0, 0), archerEntity.runningSpeed * Time.deltaTime);
            }
           


        }

        //movement.x = archerEntity.runningSpeed;

        archer.Translate(archerEntity.runningSpeed * Time.deltaTime, 0f, 0f);
        //archer.GetComponent<Rigidbody2D>().velocity = new Vector2(archerEntity.runningSpeed * Time.deltaTime, archer.GetComponent<Rigidbody2D>().velocity.y);


        //archer.GetComponent<Rigidbody2D>().MovePosition((Vector2)archer.GetComponent<Rigidbody2D>().position + (direction * archerEntity.runningSpeed * Time.deltaTime));


        //archer.GetComponent<Rigidbody2D>().AddForce(archerEntity.runningSpeed * direction * Time.deltaTime, ForceMode2D.Impulse);


        //archer.GetComponent<Rigidbody2D>().AddForce(force);
        //archer.position = Vector3.MoveTowards(archer.position, path.vectorPath[currentWaypoint], archerEntity.runningSpeed * Time.deltaTime);


        /*
      if (distance < nextWaypointDistance)
      {
          currentWaypoint++;
      }

       * 
      if (directionLookEnabled)
      {
          if(archer.GetComponent<Rigidbody2D>().velocity.x > 0.05f)
          {
              archer.transform.localScale = new Vector3(-1f * Mathf.Abs(archer.transform.localScale.x), archer.transform.localScale.y, archer.transform.localScale.z);
          }else if(archer.GetComponent<Rigidbody2D>().velocity.x < -0.05f)
          {
              archer.transform.localScale = new Vector3(Mathf.Abs(archer.transform.localScale.x), archer.transform.localScale.y, archer.transform.localScale.z);
          }
      }
      */
        return this;

    }

    IEnumerator jump()
    {
        Debug.Log("jump coroutine started");
        archer.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        float timer = 0;
        jumping = true;
        while (jumping && timer < airTime)
        {
            float percentJumpCompleted = timer / airTime;

            //this is the line that I don't think is working right and causing the jump to be floaty
            Vector2 jumpVectorThisFrame = Vector2.Lerp((Vector2.up + new Vector2(0, jumpDist)), Vector2.zero, percentJumpCompleted);
            archer.GetComponent<Rigidbody2D>().AddForce(jumpVectorThisFrame);
            timer += Time.deltaTime;
            yield return null;
        }

        jumping = false;
        Debug.Log("jump coroutine ended");
    }

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

