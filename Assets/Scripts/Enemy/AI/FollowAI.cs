using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAI : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public float sightRange = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private float sightCheck;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        sightCheck = direction.magnitude;
        direction.Normalize();
        movement = direction;
    }

    private void FixedUpdate()
    {
        moveCharacter(movement);
    }

    void moveCharacter(Vector2 direction)
    {
        if(sightCheck < sightRange)
        {
            rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
        }
    }

    //Zeichnet den Sichtradius des Gegners
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;        
    }
}
