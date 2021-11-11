using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * How to Use:
 * Objekt hat einen Collider als Trigger und einen normalen Collider
 * Der Rigidbody des Objekts darf keinen Drag besitzen
 * Das Objekt fällt sobald der Spieler den Trigger auslöst. 
 * Das Objekt verschwindet sobald es den Spieler oder den Boden berrührt und
 * erscheint sobald das Objekt außerhalb der Kamera war.
 */
public class FallingObstacle : MonoBehaviour
{
    [SerializeField] private int obstacleDamage;
    [SerializeField] private float fallSpeed = 1f;
    [SerializeField] private bool respawn = true;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector3 startPos;
    private bool isAwake = false;
    private bool isActive = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        if (isAwake && isActive)
        {
            rb.velocity = rb.velocity + new Vector2(0f, -fallSpeed);
        }

        if (!spriteRenderer.isVisible)
        {
            isActive = true;
            spriteRenderer.color = new Color(1, 1, 1, 1);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Awake");
            isAwake = true;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (obstacleDamage > 0)
            {
                collision.gameObject.GetComponent<PlayerEntity>().takeDamage(obstacleDamage);
                Respawn();
            }
        }
        else if (collision.gameObject.tag == "Ground")
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        if (respawn)
        {
            transform.position = startPos;
            rb.velocity = new Vector2(0f, 0f);
            isAwake = false;
            isActive = false;
            spriteRenderer.color = new Color(1, 1, 1, 0);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
