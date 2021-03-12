using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinCollectScript : MonoBehaviour
{
    [SerializeField] private Inventory playerInventory = null;
   
    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player"){
            if(playerInventory != null)
            {
                playerInventory.ItemContainer.addCurrency(1);
            }
            Destroy(this.gameObject);
        }
    }
}
