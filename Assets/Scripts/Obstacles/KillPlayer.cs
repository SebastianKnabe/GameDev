using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

      void OnCollisionEnter2D(Collision2D col)
    {
        col.gameObject.GetComponent<PlayerEntity>().death();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
