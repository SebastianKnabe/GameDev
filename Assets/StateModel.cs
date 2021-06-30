using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateModel : MonoBehaviour
{
    [System.Serializable]
    public class Colliders
    {
        public Transform transform;
        public Vector2 boxSize;
        public bool rayCastSpawnPoint;
    }

    [Header("shared attack attributes")]
    public float shootingCooldown;
    public float attackSpeed;
  

    

    [Header("shared player attributes")]  
    public bool playerInRange;
    public bool playerInSight;


    [Header("shared obstacle object")]
    public GameObject wallObjectsParent;
    

    [Header("shared archer attributes")]
    public float runningSpeed;
    public float attackDistance;
    public Colliders[] colliders;
    public bool blockAbility;
    public float blockProbability;
    public LayerMask blockMask;
    //public Transform groundCheck, wallCheck, heightCheck;
    //public Vector2 groundBoxSize;
    //public Vector2 wallBoxSize;
    public float maxFallHeight;
    public float maxDistanceFromSpawnPoint;
    public float timePassUntilMoveBack;
    public float timePassFlipChar;
    public float timePassUntilRetargeting;






    private bool checkingWall, isGrounded = false;
    private Vector3 middlePoint;
    private Animator animator;
    private int layerMask;
    private Transform player;
    private Vector3 spawnPosition;
    private Vector3 lastPlayerPosition;
    private float timeSinceAwayFromSpawn;
    private float timeSinceLastShot;
    private float timeSinceLastFlip;
    private float timeSinceLastRetargeting;
    private bool facingRight;
    private bool isChasing;
    


   
    public Transform Player { get => player; set => player = value; }
    
    public Vector3 LastPlayerPosition { get => lastPlayerPosition; set => lastPlayerPosition = value; }
    public bool CheckingWall { get => checkingWall; set => checkingWall = value; }
    public bool IsGrounded { get => isGrounded; set => isGrounded = value; }
    public Vector3 MiddlePoint { get => middlePoint; set => middlePoint = value; }
    public Animator Animator { get => animator; set => animator = value; }
    public int LayerMask { get => layerMask; set => layerMask = value; }
    public float TimeSinceLastShot { get => timeSinceLastShot; set => timeSinceLastShot = value; }
    public Vector3 SpawnPosition { get => spawnPosition; set => spawnPosition = value; }
    public float TimeSinceAwayFromSpawn { get => timeSinceAwayFromSpawn; set => timeSinceAwayFromSpawn = value; }
    public bool FacingRight { get => facingRight; set => facingRight = value; }
    public bool IsChasing { get => isChasing; set => isChasing = value; }
    public float TimeSinceLastFlip { get => timeSinceLastFlip; set => timeSinceLastFlip = value; }
    public float TimeSinceLastRetargeting { get => timeSinceLastRetargeting; set => timeSinceLastRetargeting = value; }
    

    void Start()
    {
        // to avoid that the enemy has cooldown before the first shot
        //layerMask = LayerMask.NameToLayer("Platform");
        layerMask = 1 << 8;
        timeSinceLastShot = shootingCooldown;
        //TimeSinceLastRetargeting = TimePassUntilRetargeting;
        playerInRange = false;
        playerInSight = false;
        facingRight = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        spawnPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (TimeSinceLastShot < shootingCooldown)
        {
            TimeSinceLastShot += Time.fixedDeltaTime;
        }

        if (TimeSinceLastFlip < timePassFlipChar)
        {
            TimeSinceLastFlip += Time.fixedDeltaTime;
        }

        if(Mathf.Abs(transform.position.x - SpawnPosition.x) > 0.1f && !isChasing)
        {
            isChasing = true; 
            TimeSinceAwayFromSpawn = 0;
            timeSinceLastRetargeting = 0;
        }

        if(timeSinceLastRetargeting < timePassUntilRetargeting && returnToSpawnPoint())
        {
            timeSinceLastRetargeting += Time.fixedDeltaTime;
        }

        // also refresh timer cooldown if player is seen while the enemy is running back to the spawn point
        if (returnToSpawnPoint() && playerInSight && TimeSinceLastRetargeting >= timePassUntilRetargeting)
        {
            TimeSinceAwayFromSpawn = 0f;
            TimeSinceLastRetargeting = 0f;
        }

    }

    public void Flip()
    {


            if (!isGrounded)
            {
                return;
            }
            if (transform.position.x > lastPlayerPosition.x)
            {
                FacingRight = false;
                transform.eulerAngles = new Vector3(0, 180, 0);

            }
            else
            {
                FacingRight = true;
                transform.eulerAngles = new Vector3(0, 0, 0);

            }
        
    }

    public bool returnToSpawnPoint()
    {

        if(isChasing && timeSinceAwayFromSpawn >= timePassUntilMoveBack)
        {
            lastPlayerPosition = spawnPosition;
            return true;
        }
   
        return false;
        
    }

    public bool BlockProjectile()
    {

        if (!blockAbility)
        {
            return false;
        }
        Collider2D[] hitInfo = Physics2D.OverlapBoxAll(colliders[3].transform.position, colliders[3].boxSize, 0, blockMask);
        foreach (Collider2D col in hitInfo)
        {
            if (col.gameObject.CompareTag("Bullet"))
            {
                
                if (Random.value <= blockProbability)
                {
                    Debug.Log("Block");
                    Destroy(col.gameObject);
                    return true;
                }
            }
        }

        return false;
    }


    void OnDrawGizmos()
    {
      
        foreach (Colliders col in colliders)
        {
            if (col.rayCastSpawnPoint)
            {
                //draw raycast
                Gizmos.color = Color.red;
                Debug.DrawRay(col.transform.position, Vector2.down * col.boxSize.y, Color.red, 0, false); ;
            }
            else
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawCube(col.transform.position, new Vector3(col.boxSize.x, col.boxSize.y, 0));
            }
        }

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        Gizmos.color = Color.magenta;


        if(maxDistanceFromSpawnPoint > 0)
        {
            Gizmos.DrawWireSphere(spawnPosition, maxDistanceFromSpawnPoint);

        }


    }


}
