using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    [SerializeField] LayerMask platformMask;

    private BoxCollider2D boxCollider2D;
    private float rayCastOffset = 0.05f;

    // Start is called before the first frame update
    void Start() {
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update() {
        Jump();

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * moveSpeed;
    }

    void Jump() {
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            Debug.Log("JUMP");
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, rayCastOffset, platformMask);

        Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }
}
