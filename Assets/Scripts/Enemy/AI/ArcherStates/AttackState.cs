using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public Transform archer;
    public IdleState idleState;
    public ChaseState chaseState;
    public GameObject arrowPrefab;
    public Transform arrowStart;
    public float arrowSpeed = 60.0f;
    public string animatorStringForAttack;



    StateModel archerEntity;
    private string stateString = "attackState";

    public void Start()
    {
        archerEntity = archer.GetComponent<StateModel>();
        runFixedUpdate = false;
    }


    public override State RunCurrentState()
    {

        archerEntity.Flip();

        if (archerEntity.PlayerInRange && !archerEntity.PlayerInSight)
        {
            return chaseState;
        }else if (!archerEntity.PlayerInSight)
        {
            resetAttackAnimationSpeed();
            return idleState;
        }
        else if(archerEntity.PlayerInRange && archerEntity.PlayerInSight)
        {
            //attack
           

            if (!DialogueManager.dialogueMode)
            {
                increaseAttackAnimationSpeed();
                archerEntity.Animator.Play(animatorStringForAttack);
            }
         


            if (archerEntity.Animator.GetCurrentAnimatorStateInfo(0).IsName(animatorStringForAttack) &&
                archerEntity.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {

                Vector3 enemyPlayerDistance = archerEntity.Player.position - arrowStart.position;
                Debug.Log(enemyPlayerDistance + "      " + arrowStart.position);
                float rotationZ = Mathf.Atan2(enemyPlayerDistance.y, enemyPlayerDistance.x) * Mathf.Rad2Deg;
                float distance = enemyPlayerDistance.magnitude;
                Vector2 direction = enemyPlayerDistance / distance;
                direction.Normalize();
                fireBullet(direction, rotationZ);

                archerEntity.TimeSinceLastShot = 0f;
                resetAttackAnimationSpeed();
                return idleState;
            }





        }
        return this;
    }


    
    void increaseAttackAnimationSpeed()
    {
        archerEntity.Animator.speed = archerEntity.AttackSpeed;
    }

    void resetAttackAnimationSpeed()
    {
        archerEntity.Animator.speed = 1;
    }

    void fireBullet(Vector2 direction, float rotationZ)
    {
        GameObject b = Instantiate(arrowPrefab) as GameObject;
        b.transform.position = arrowStart.position;
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        b.GetComponent<Rigidbody2D>().velocity = direction * arrowSpeed;
    }

    public override string getStateType()
    {
        return stateString;
    }
}
