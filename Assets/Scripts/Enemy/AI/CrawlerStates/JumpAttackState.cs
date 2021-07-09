using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttackState : State
{
    public PatrollingState patrollingState;
    public string animatorStringForJump;
    public Transform enemy;
    public float jumpHeight;
    
    
    private StateModel enemyEntity;
    private Rigidbody2D enemyRB;
    private string stateString = "jumpAttackState";
    private bool started = false;
    private bool enemyFelt = false;
    private Coroutine jmpRoutine;

    public void Start()
    {
        enemyEntity = enemy.GetComponent<StateModel>();
        enemyRB = enemyEntity.GetComponent<Rigidbody2D>();
    }

    public override string getStateType()
    {
        return stateString;
    }

    public override State RunCurrentState()
    {

        if(!started)
        {
            enemyEntity.Animator.Play(animatorStringForJump);
            float distanceFromPlayer = enemyEntity.Player.transform.position.x - enemyEntity.transform.position.x;
            //Debug.Log(sneakerRigidbody.velocity);
            enemyRB.AddForce(new Vector2(distanceFromPlayer, jumpHeight), ForceMode2D.Impulse);
            started = true;
        }
        else
        {
            bool isGrounded = Physics2D.OverlapBox(enemyEntity.colliders[1].transform.position, enemyEntity.colliders[1].boxSize, 0, enemyEntity.LayerMask);
            if (isGrounded)
            {
                started = false;
                return patrollingState;
            }
        }

        return this;
    }

  
}
