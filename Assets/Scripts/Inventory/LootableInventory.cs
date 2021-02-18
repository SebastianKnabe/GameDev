using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootableInventory : MonoBehaviour
{
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private GameObject TextObject;
    [SerializeField] private GameObject TextPosition;
    [SerializeField] private int currency = 0;
    [SerializeField] private Animator animator;
    [SerializeField] private ItemSlot[] items = new ItemSlot[2];

    private bool playerInRange = false;
    private GameObject instanceOfTextObject;
    private bool isLootable = true;

    void Start()
    {
        playerInRange = false;
        TextObject.GetComponent<TextMesh>().text = "Press [E] to loot";
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && playerInRange && isLootable)
        {
            Debug.Log("LootItems");

            if(animator != null)
            {
                animator.SetTrigger("isLooted");
            }

            playerInventory.addCurrency(currency);
            for(int i = 0; i < items.Length; i++)
            {
                playerInventory.ItemContainer.AddItem(items[i]);
                
            }
            isLootable = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            instanceOfTextObject = Instantiate(TextObject, TextPosition.transform.position, Quaternion.identity, TextPosition.transform);
            playerInRange = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {


        if (other.tag == "Player")
        {
            Destroy(instanceOfTextObject);
            playerInRange = false;
        }
    }
}
