using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public Transform archer;
    public IdleState idleState;
    public ChaseState chaseState;
    public string[] animatorStringForAttack;

    [Header("ranged attributes")]
    public GameObject arrowPrefab;
    public Transform arrowStart;
    public float arrowSpeed = 60.0f;
    public bool ranged;

    [Header("melee attributes")]
    public Transform damageArea;
    public int damageRadius;
    public int[] damagePerAttack;
    public LayerMask layerMask;



    StateModel archerEntity;
    private string stateString = "attackState";
    private int currentAttackIndex;
    private float animationEndTime;
    private bool successfullyDealtDamage = false;
    public void Start()
    {
        archerEntity = archer.GetComponent<StateModel>();
        currentAttackIndex = 0;
        animationEndTime = ranged ? 1.0f : 0.5f;
    }


    public override State RunCurrentState()
    {
        archerEntity.TimeSinceLastShot = 0;
        archerEntity.Flip();

        if (archerEntity.playerInRange && !archerEntity.playerInSight)
        {
            currentAttackIndex = 0;
            successfullyDealtDamage = false;
            return chaseState;
        }else if (!archerEntity.playerInSight)
        {
            resetAttackAnimationSpeed();
            currentAttackIndex = 0;
            successfullyDealtDamage = false;
            return idleState;
        }
        else if(archerEntity.playerInRange && archerEntity.playerInSight)
        {
            //attack
           

            if (!DialogueManager.dialogueMode)
            {
                increaseAttackAnimationSpeed();
                archerEntity.Animator.Play(animatorStringForAttack[currentAttackIndex]);
            }
         


            if (archerEntity.Animator.GetCurrentAnimatorStateInfo(0).IsName(animatorStringForAttack[currentAttackIndex]) &&
                archerEntity.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= animationEndTime)
            {

                attack();

                if(currentAttackIndex + 1 >= animatorStringForAttack.Length || !ranged && !successfullyDealtDamage)
                {
                    currentAttackIndex = 0;
                    successfullyDealtDamage = false;
                    return idleState;
                }
                else
                {
                    successfullyDealtDamage = false;
                    currentAttackIndex++;
                }

            }





        }
        return this;
    }


    private void attack()
    {
        if (ranged)
        {
            shootProjectile();
        }
        else
        {
            meleeAttack();
        }
    }

    private void meleeAttack()
    {

        Collider2D[] hitInfo = Physics2D.OverlapCircleAll(damageArea.transform.position, damageRadius, layerMask);
        foreach(Collider2D col in hitInfo){
            Debug.Log(col);
            if (col != null && col.gameObject.CompareTag("Player"))
            {
                col.gameObject.GetComponent<PlayerEntity>().takeDamage(damagePerAttack[currentAttackIndex]);
                successfullyDealtDamage = true;
            }
        }
        
       
       


    }

    private void shootProjectile()
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
    }

    
    void increaseAttackAnimationSpeed()
    {
        archerEntity.Animator.speed = archerEntity.attackSpeed;
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

    void OnDrawGizmos()
    {

        if(damageArea != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(damageArea.position, damageRadius);

        }

      
    }

}
