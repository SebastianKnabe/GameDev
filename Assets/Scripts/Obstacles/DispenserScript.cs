using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispenserScript : MonoBehaviour
{
    public TriggerObstacle obstaclePrefab;
    public float obstacleSpeed;
    public Transform fireSpawn;
    public float obstacleDelay;
    public float variance;
    [SerializeField] private bool resetPlayer;

    private bool shoot;
    private float time = 0;
    private Vector2 dir;

    // Start is called before the first frame update
    void Start()
    {
        dir = new Vector3(transform.rotation.x * obstacleSpeed, transform.rotation.y * obstacleSpeed, transform.rotation.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (shoot == true)
        {
            if (time < Time.time)
            {
                TriggerObstacle obstacle = Instantiate(obstaclePrefab, fireSpawn.position, transform.rotation) as TriggerObstacle;
                obstacle.GetComponent<DispenserMunitionDamagePlayer>().resetPlayer = resetPlayer;
                obstacle.dir = new Vector2(obstacle.transform.up.x + variance, obstacle.transform.up.y) * obstacleSpeed;
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
