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




    StateModel archerEntity;
    private Animator attackAnimation;
    private string stateString = "attackState";

    public void Start()
    {
        archerEntity = archer.GetComponent<StateModel>();
        attackAnimation = archer.GetComponent<Animator>();
    }

    public override State RunCurrentState()
    {


        if (archerEntity.getPlayerInRange() && !archerEntity.getPlayerInSight())
        {
            return chaseState;
        }else if (!archerEntity.getPlayerInSight())
        {
            resetAttackAnimationSpeed();
            return idleState;
        }
        else if(archerEntity.getPlayerInRange() && archerEntity.getPlayerInSight())
        {
            //attack
           

            if (!DialogueManager.dialogueMode)
            {
                increaseAttackAnimationSpeed();
                attackAnimation.Play("archer_enemy_shoot");
            }
         


            if (attackAnimation.GetCurrentAnimatorStateInfo(0).IsName("archer_enemy_shoot") &&
                attackAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {

                Vector3 enemyPlayerDistance = archerEntity.getPlayerTransform().position - arrowStart.position;
                Debug.Log(enemyPlayerDistance + "      " + arrowStart.position);
                float rotationZ = Mathf.Atan2(enemyPlayerDistance.y, enemyPlayerDistance.x) * Mathf.Rad2Deg;
                float distance = enemyPlayerDistance.magnitude;
                Vector2 direction = enemyPlayerDistance / distance;
                direction.Normalize();
                fireBullet(direction, rotationZ);

                archerEntity.timeSinceLastShot = 0f;
                resetAttackAnimationSpeed();
                return idleState;
            }





        }
        return this;
    }

    
    void increaseAttackAnimationSpeed()
    {
            attackAnimation.speed = archerEntity.attackSpeed;
    }

    void resetAttackAnimationSpeed()
    {
        attackAnimation.speed = 1;
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
