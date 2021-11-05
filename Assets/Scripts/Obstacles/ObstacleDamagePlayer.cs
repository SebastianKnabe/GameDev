using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDamagePlayer : MonoBehaviour
{
    // Start is called before the first frame update

    public float damageTaken;


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerEntity>().hitObstacle(damageTaken);
        }
    }


}
