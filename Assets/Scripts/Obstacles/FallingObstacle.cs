using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * How to Use:
 * Objekt hat Rigibody2D mit Sleeping Mode = Start Asleep
 * Sobald Trigger ausgelöst wird, fällt das Objekt mit den Rigibody2D Gravitations-Einstellungen
 */
public class FallingObstacle : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private int obstacleDamage;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            rb.WakeUp();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (obstacleDamage > 0)
            {
                collision.gameObject.GetComponent<PlayerEntity>().takeDamage(obstacleDamage);
                Destroy(this.gameObject);
            }
        }
        else if (collision.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
    }
}
