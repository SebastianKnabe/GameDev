using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float sprintSpeed = 30f;
    public float jumpVelocity;
    public float backToGroundSpeed = -0.3f;
    public float gravity = -0.4f;
    public float speed;
    public AudioSource audioSource;


    [SerializeField] LayerMask platformMask;
    [SerializeField] private AudioClip jumpSound;

    private BoxCollider2D boxCollider2D;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteManager spriteManager;
    private float rayCastOffset = 0.05f;
    private static staminaRefillScript staminaRefillScript;
    private Vector2 backupPosition;
    private float coyoteTimer;
    private float coyoteFrames = 10;
    private bool hasJumped = false;
    private List<Vector3> groundedPosition = new List<Vector3>();
    private bool safeSpawn = true;


    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        staminaRefillScript = GameObject.FindObjectOfType<staminaRefillScript>();
        animator = gameObject.GetComponent<Animator>();
        spriteManager = gameObject.GetComponent<SpriteManager>();
        speed = moveSpeed;
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector2(0f, gravity), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

        if (DialogueManager.dialogueMode)
        {
            animator.SetFloat("movementSpeed", 0);
            staminaRefillScript.requestSprint(false);
            return;
        }
        if (isGrounded())
        {
            setBackupPosition();
            setSpawnPointAfterSpikeHit(gameObject.transform.position);
            if (Input.GetKey(KeyCode.LeftShift) && staminaRefillScript.staminaUI.fillAmount > 0 && staminaRefillScript.requestSprint(true))
            {
                speed = sprintSpeed;
            
            }
            else
            {
                if (hasJumped)
                {
                    hasJumped = false;
                }
                speed = moveSpeed;
                staminaRefillScript.requestSprint(false);
            }
            coyoteTimer = 0;

        }
        else
        {
            coyoteTimer++;
            staminaRefillScript.requestSprint(false);
        }
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal") * speed, rb.velocity.y, 0f);
        rb.velocity = movement;
        animator.SetFloat("movementSpeed", movement.magnitude);
        flipSprite();

        Jump();
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && !hasJumped && (isGrounded() || coyoteTimer < coyoteFrames))
        {
            //animator.SetBool("isJumping", true);
            hasJumped = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity;
            audioSource.PlayOneShot(jumpSound);
        }
        animator.SetBool("isJumping", !isGrounded());
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, rayCastOffset, platformMask);

        Debug.DrawRay(boxCollider2D.bounds.center, Vector2.down);

        //Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }

    private void flipSprite()
    {
        bool facingRight = spriteManager.getFacingRight();
        if (rb.velocity.x < 0 && facingRight)
        {
            spriteManager.rotateSprite();
        }
        else if (rb.velocity.x > 0 && !facingRight)
        {
            spriteManager.rotateSprite();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            transform.parent = collision.gameObject.transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            transform.parent = null;
        }
    }


    private void setBackupPosition()
    {
        RaycastHit2D raycastHitLeft = Physics2D.BoxCast(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.size.x, 0f, 0f), boxCollider2D.bounds.size, 0f, Vector2.down, rayCastOffset, platformMask);
        RaycastHit2D raycastHitRight = Physics2D.BoxCast(boxCollider2D.bounds.center + new Vector3(boxCollider2D.bounds.size.x, 0f, 0f), boxCollider2D.bounds.size, 0f, Vector2.down, rayCastOffset, platformMask);

        if (raycastHitLeft.collider != null)
        {
            backupPosition = transform.position - new Vector3(boxCollider2D.bounds.size.x, 0f, 0f);
        }
        else if (raycastHitRight.collider != null)
        {
            backupPosition = transform.position + new Vector3(boxCollider2D.bounds.size.x, 0f, 0f);
        }
    }

    public void resetPosition()
    {
        transform.position = backupPosition;
        rb.velocity = new Vector2(0f, 0f);
    }

    public Vector2 getBackupPosition()
    {
        return backupPosition;
    }

    public void setSpawnPointAfterSpikeHit(Vector3 currentGroundedPosition)
    {
        if (safeSpawn)
        {

            if (groundedPosition.Count == 20)
            {
                gameObject.GetComponent<PlayerEntity>().SetNextSpawn(getSpawnPointAfterSpikeHit());
                groundedPosition.RemoveAt(0);
                groundedPosition.Insert(19, currentGroundedPosition);
            }
            else
            {
                groundedPosition.Add(currentGroundedPosition);

            }
        }

    }

    public Vector3 getSpawnPointAfterSpikeHit()
    {
        return groundedPosition[0];
    }

    public void setSafeSpawn(bool safe)
    {
        safeSpawn = safe;
    }
}
