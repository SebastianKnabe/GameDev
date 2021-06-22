using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispenserScript : MonoBehaviour
{

    public TriggerObstacle obstaclePrefab;
    public float obstacleSpeed;
    private float time = 0;
    public float obstacleDelay;

    private bool shoot;

    private Vector2 dir;

    // Start is called before the first frame update
    void Start()
    {
        dir = new Vector2(obstacleSpeed, 0);

        
    }

    // Update is called once per frame
    void Update()
    {
        if (shoot == true)
        {
            if (time < Time.time)
            {
                TriggerObstacle obstacle = Instantiate(obstaclePrefab, transform.position - new Vector3(2, 0, 0), transform.rotation) as TriggerObstacle;
                obstacle.dir = dir;
                time = Time.time + obstacleDelay;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            shoot = true;
        } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            shoot = false;
        }
    }
}
