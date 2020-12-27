using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    
     public float damage = 1.0f;

     private void OnTriggerEnter2D(Collider2D other){
    
        if(other.tag == "Player"){
          
           other.gameObject.GetComponent<PlayerEntity>().takeDamage(damage);
           
        }
    }
}
