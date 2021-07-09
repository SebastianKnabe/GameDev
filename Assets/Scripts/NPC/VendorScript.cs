using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorScript : MonoBehaviour
{
    [SerializeField] private GameObject TextObject;
    [SerializeField] private GameObject TextPosition;
    [SerializeField] private InventoryEvent inventoryEvent;
    [SerializeField] private int currency = 100;
    [SerializeField] private Inventory inventory;
    [SerializeField] private ItemSlot[] items = new ItemSlot[2];

    private bool playerInRange = false;
    private GameObject instanceOfTextObject;
    private ItemContainer itemContainer = new ItemContainer(20);

    void Start()
    {
        playerInRange = false;
        initItemContainer();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && playerInRange)
        {
            Debug.Log("LootItems");

            //Raise Event
            inventory.ItemContainer = itemContainer;
            inventory.inventoryType = InventoryType.Vendor;
            inventoryEvent.Raise(inventory);

            //if container is empty dont allow player to open chest again
            if (!itemContainer.hasItems())
            {
                Destroy(instanceOfTextObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            TextObject.GetComponent<TextMesh>().text = "Press [E] to shop";
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

    private void initItemContainer()
    {
        for (int i = 0; i < items.Length; i++)
        {
            itemContainer.AddItem(items[i]);
        }
        itemContainer.Currency = currency;
    }
}
