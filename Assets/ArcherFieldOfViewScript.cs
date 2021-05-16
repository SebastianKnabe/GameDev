using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherFieldOfViewScript : MonoBehaviour
{
    public Transform castPoint; 
    public bool playerInRange;
    public bool playerInSight;
    public Transform player;
    //public float radius;
    int layerMask;

    // Start is called before the first frame update
    void Start()
    {

        playerInRange = false;
        playerInSight = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        layerMask = 1 << LayerMask.NameToLayer("Platform");
        //layerMask = 1 << 8;
        //radius = GetComponent<CircleCollider2D>().radius;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;
            playerInSight = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //Debug.DrawRay(castPoint.position, player.position - castPoint.position, Color.red, 1, false);

        if (playerInRange)
        {
            RaycastHit2D hit = Physics2D.Linecast(castPoint.position, player.position, layerMask);
            if (!hit)
            {
                playerInSight = true;
            }
            else
            {
                playerInSight = false;
            }

        }
        
    }

    public bool getPlayerInRange()
    {
        return playerInRange;
    }
    public bool getPlayerInSight()
    {
        return playerInSight;
    }
    public Transform getPlayerTransform()
    {
        return player;
    }
}
