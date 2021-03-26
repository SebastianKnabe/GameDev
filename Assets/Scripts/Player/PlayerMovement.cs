﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float sprintSpeed = 20f;
    public float jumpForce = 30f;
    public float backToGroundSpeed = -0.3f;
    public float gravity = -0.4f;

    [SerializeField] LayerMask platformMask;

    private BoxCollider2D boxCollider2D;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteManager spriteManager;
    private float rayCastOffset = 0.05f;
    private static staminaRefillScript staminaRefillScript;
    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        staminaRefillScript = GameObject.FindObjectOfType<staminaRefillScript>();
        animator = gameObject.GetComponent<Animator>();
        spriteManager = gameObject.GetComponent<SpriteManager>();
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

        float speed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift) && staminaRefillScript.staminaUI.fillAmount > 0 && staminaRefillScript.requestSprint(true))
        {
            speed = sprintSpeed;
        }
        else
        {
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
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            //animator.SetBool("isJumping", true);
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.S) && !isGrounded())
        {
            rb.AddForce(new Vector2(0f, backToGroundSpeed), ForceMode2D.Impulse);
        }
        animator.SetBool("isJumping", !isGrounded());
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, rayCastOffset, platformMask);

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
}
