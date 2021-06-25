using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDamagePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

      void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Obstacle")
        {
            col.gameObject.GetComponent<PlayerEntity>().hitObstacle();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerMovement>().setSafeSpawn(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerMovement>().setSafeSpawn(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
