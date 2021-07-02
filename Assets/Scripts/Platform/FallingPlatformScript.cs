using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformScript : MonoBehaviour
{

    public Rigidbody2D rb;
    public float boundValue;
    Vector2 startPos;
    public bool respawns = true;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(transform.position.y <= boundValue)
        {
            if (!respawns)
            {
                Destroy(gameObject);
            }

            rb.isKinematic = true;
            rb.velocity = new Vector3(0, 0, 0);
            transform.position = startPos;

        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {

            Vector3 direction = transform.position - collision.gameObject.transform.position;
            if (direction.y < 0)
            {
                rb.isKinematic = false;
            }

        }
    }

   


}
