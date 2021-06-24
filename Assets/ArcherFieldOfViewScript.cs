using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ArcherFieldOfViewScript : MonoBehaviour
{
    public GameObject archer;
    public Transform castPoint;
    public Tilemap tilemapGround;


    public Transform player;
    //public float radius;
    int layerMask;
    StateModel archerEntity;
    // Start is called before the first frame update
    void Start()
    {

        archerEntity = archer.GetComponent<StateModel>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        layerMask = layerMask = 1 << 8;


        //layerMask = layerMask = 1 << 8;
        //radius = GetComponent<CircleCollider2D>().radius;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            archerEntity.playerInRange = true;
           

        }
    }


    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            archerEntity.playerInRange = false;
            archerEntity.playerInSight = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    
            

        if (archerEntity.playerInRange)
        {
            Debug.DrawRay(castPoint.position, player.position - castPoint.position, Color.red, 0, false);
            RaycastHit2D hit = Physics2D.Linecast(castPoint.position, player.position, layerMask);
            if (!hit)
            {
                archerEntity.playerInSight = true;
                archerEntity.LastPlayerPosition = archerEntity.Player.transform.position;
               
            }
            else
            {
                archerEntity.playerInSight = false;

            }

        }
        
    }



}
