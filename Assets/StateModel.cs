using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateModel : MonoBehaviour
{


    [Header("shared attack attributes")]
    [SerializeField]
    private float shootingCooldown;
    [SerializeField]
    private float attackSpeed;
  

    

    [Header("shared player attributes")]
    
    [SerializeField]
    private bool playerInRange;
    [SerializeField]
    private bool playerInSight;


    [Header("shared obstacle object")]
    [SerializeField]
    private GameObject wallObjectsParent;


    [Header("shared archer attributes")]
    [SerializeField]
    private float runningSpeed;
    [SerializeField]
    private float attackDistance;
    [SerializeField]
    private float circleRadius = 0.2f;
    [SerializeField]
    private Transform groundCheck, wallCheck;
    [SerializeField]
    private Vector2 boxSize;
    [SerializeField]
    private float maxDistanceFromSpawnPoint;
    [SerializeField]
    private float timePassUntilMoveBack;
    [SerializeField]
    private float timePassFlipChar;






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
    private bool facingRight;
    private bool isChasing;
    public float ShootingCooldown { get => shootingCooldown; set => shootingCooldown = value; }
    public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public Transform Player { get => player; set => player = value; }
    public float RunningSpeed { get => runningSpeed; set => runningSpeed = value; }
    public float AttackDistance { get => attackDistance; set => attackDistance = value; }
    public bool PlayerInRange { get => playerInRange; set => playerInRange = value; }
    public bool PlayerInSight { get => playerInSight; set => playerInSight = value; }
    public Vector3 LastPlayerPosition { get => lastPlayerPosition; set => lastPlayerPosition = value; }
    public GameObject WallObjectsParent { get => wallObjectsParent; set => wallObjectsParent = value; }
    public float CircleRadius { get => circleRadius; set => circleRadius = value; }
    public Transform GroundCheck { get => groundCheck; set => groundCheck = value; }
    public Vector2 BoxSize { get => boxSize; set => boxSize = value; }
    public Transform WallCheck { get => wallCheck; set => wallCheck = value; }
    public bool CheckingWall { get => checkingWall; set => checkingWall = value; }
    public bool IsGrounded { get => isGrounded; set => isGrounded = value; }
    public Vector3 MiddlePoint { get => middlePoint; set => middlePoint = value; }
    public Animator Animator { get => animator; set => animator = value; }
    public int LayerMask { get => layerMask; set => layerMask = value; }
    public float TimeSinceLastShot { get => timeSinceLastShot; set => timeSinceLastShot = value; }
    public float MaxDistanceFromSpawnPoint { get => maxDistanceFromSpawnPoint; set => maxDistanceFromSpawnPoint = value; }
    public Vector3 SpawnPosition { get => spawnPosition; set => spawnPosition = value; }
    public float TimePassUntilMoveBack { get => timePassUntilMoveBack; set => timePassUntilMoveBack = value; }
    public float TimeSinceAwayFromSpawn { get => timeSinceAwayFromSpawn; set => timeSinceAwayFromSpawn = value; }
    public bool FacingRight { get => facingRight; set => facingRight = value; }
    public bool IsChasing { get => isChasing; set => isChasing = value; }
    public float TimeSinceLastFlip { get => timeSinceLastFlip; set => timeSinceLastFlip = value; }
    public float TimePassFlipChar { get => timePassFlipChar; set => timePassFlipChar = value; }

    void Start()
    {
        // to avoid that the enemy has cooldown before the first shot
        //layerMask = LayerMask.NameToLayer("Platform");
        layerMask = 1 << 8;
        timeSinceLastShot = ShootingCooldown;
        playerInRange = false;
        playerInSight = false;
        facingRight = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        spawnPosition = transform.position;

    }

    void FixedUpdate()
    {
        if (TimeSinceLastShot < ShootingCooldown)
        {
            TimeSinceLastShot += Time.fixedDeltaTime;
        }

        if (TimeSinceLastFlip < TimePassFlipChar)
        {
            TimeSinceLastFlip += Time.fixedDeltaTime;
        }

      
    }

    public void Flip()
    {
       
        
            
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

        if(isChasing && timeSinceAwayFromSpawn >= TimePassUntilMoveBack)
        {
            lastPlayerPosition = spawnPosition;
            return true;
        }
   
        return false;
        
    }


}
