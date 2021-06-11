using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : State
{
    public ChaseState chaseState;
    public IdleState idleState;

    public string animatorStringForJump;
    public string animatorStringForFall;

    public Transform archer;
    public float speed;
    StateModel archerEntity;

    private string stateString = "jumpState";

    private bool started = false;
    private bool playerFelt = false;
    private Coroutine jmpRoutine;

    
    public void Start()
    {
        archerEntity = archer.GetComponent<StateModel>();
        runFixedUpdate = true ;
    }

    public override State RunCurrentState()
    {

        //archerEntity.CheckingWall = Physics2D.OverlapCircle(archerEntity.WallCheck.position, archerEntity.CircleRadius, archerEntity.LayerMask);
        archerEntity.IsGrounded = Physics2D.OverlapBox(archerEntity.groundCheck.position, archerEntity.groundBoxSize, 0, archerEntity.LayerMask);


        if (started && playerFelt && archerEntity.IsGrounded)
        {
            playerFelt = false;
            started = false;
            StopCoroutine(jmpRoutine);
            return idleState;
           
        }

        if (!started)
        {
            started = true;
            jmpRoutine = StartCoroutine(JumpAnimation(archer.position, archerEntity.MiddlePoint));
        }




        return this;

    }


    IEnumerator JumpAnimation(Vector3 fromPos, Vector3 toPos)
    {

        float duration = (toPos - fromPos).magnitude * speed;


        Vector3 fromPos01 = fromPos;
        Vector3 toPos01 = new Vector3(fromPos.x + ((toPos - fromPos).x / 2), toPos.y +  ((toPos).y * 1/8f), fromPos.z);
        //Vector3 toPos01 = new Vector3(fromPos.x + ((toPos - fromPos).x / 2), toPos.y +  ((toPos - fromPos).y * 1.05f), fromPos.z);

        //Debug.Log("toPos01 : " + toPos01);

        float tempT;

        for (float t = 0; t < 1.0f; t += Time.fixedDeltaTime / duration * 0.5f)
        {
            archerEntity.Animator.Play(animatorStringForJump);

            //tempT = Mathf.Sin(t * Mathf.PI * 0.5f);
            tempT = Mathf.Sin(t * Mathf.PI * 0.5f);
           
            archer.GetComponent<Rigidbody2D>().position = Vector3.Lerp(fromPos01, toPos01, t);

            yield return null;
        }

        Vector3 fromPos02 = toPos01;
        Vector3 toPos02 = toPos;

        for (float t = 0; t < 1.0f; t += Time.fixedDeltaTime / duration * 0.5f)
        {
            //Debug.Log("FIRST LOOP");

            archerEntity.Animator.Play(animatorStringForFall);
            playerFelt = true;

            tempT = 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
            archer.GetComponent<Rigidbody2D>().position = Vector3.Lerp(fromPos02, toPos02, t);

            yield return null;
        }

        archer.GetComponent<Rigidbody2D>().position = toPos;

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

