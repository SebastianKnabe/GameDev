using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SeakerStateModel : MonoBehaviour
{

    [System.Serializable]
    public class T
{
    public Transform trans;
    public Vector2 size;
}

[Header("shared player attributes")]
    public bool playerInRange;

    [Header("shared archer attributes")]
    public float runningSpeed;
    public float attackDistance;
    public Transform groundCheck, wallCheck;
    public Vector2 groundBoxSize;
    public Vector2 wallBoxSize;

    [Header("TEST")]
    [SerializeField]
    public T[] trans;






    private bool checkingWall, isGrounded;
    private Animator animator;
    private int layerMask;
    private Transform player;
    private bool facingRight;



    public Transform Player { get => player; set => player = value; }

    public bool CheckingWall { get => checkingWall; set => checkingWall = value; }
    public bool IsGrounded { get => isGrounded; set => isGrounded = value; }
    public Animator Animator { get => animator; set => animator = value; }
    public int LayerMask { get => layerMask; set => layerMask = value; }
    public bool FacingRight { get => facingRight; set => facingRight = value; }


    void Start()
    {
        // to avoid that the enemy has cooldown before the first shot
        //layerMask = LayerMask.NameToLayer("Platform");
        layerMask = 1 << 8;
        //TimeSinceLastRetargeting = TimePassUntilRetargeting;
        playerInRange = false;
        facingRight = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        //updateDirection();

    }

    void FixedUpdate()
    {
      

    }

    public void updateDirection()
    {


        if (FacingRight)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);

        }
        else
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);

        }

    }




    void OnDrawGizmos()
    {
        
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(groundCheck.position, new Vector3(groundBoxSize.x, groundBoxSize.y, 0));

        Gizmos.DrawCube(wallCheck.position, new Vector3(wallBoxSize.x, wallBoxSize.y, 0));
        Gizmos.color = Color.cyan;
  

  


    }


}
