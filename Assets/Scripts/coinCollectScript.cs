using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinCollectScript : MonoBehaviour
{
    [SerializeField] private Inventory playerInventory = null;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("CoinTrigger: " + other.tag);
            if (playerInventory != null)
            {
                playerInventory.ItemContainer.addCurrency(1);
            }
            Destroy(this.gameObject);
        }
    }
}
