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
        col.gameObject.GetComponent<PlayerEntity>().hitObstacle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
