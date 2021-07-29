using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootableInventory : MonoBehaviour
{
    [SerializeField] private GameObject TextObject;
    [SerializeField] private GameObject TextPosition;
    [SerializeField] private InventoryEvent inventoryEvent;
    [SerializeField] private int currency = 0;
    [SerializeField] private Animator animator;
    [SerializeField] private Inventory inventory;
    [SerializeField] private ItemSlot[] items = new ItemSlot[2];

    private bool playerInRange = false;
    private GameObject instanceOfTextObject;
    private bool isLootable = true;
    private ItemContainer itemContainer = new ItemContainer(20);

    void Start()
    {
        playerInRange = false;
        initItemContainer();
    }

    private void FixedUpdate()
    {
        //if container is empty dont allow player to open chest again
        if (!itemContainer.hasItems())
        {
            isLootable = false;
            Destroy(instanceOfTextObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && playerInRange && isLootable)
        {
            Debug.Log("LootItems");

            if (animator != null)
            {
                animator.SetTrigger("isLooted");
            }

            //Raise Event
            inventory.ItemContainer = itemContainer;
            inventory.inventoryType = InventoryType.Container;
            inventoryEvent.Raise(inventory);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && isLootable)
        {
            TextObject.GetComponent<TextMesh>().text = "Press [E] to loot";
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
        itemContainer.addCurrency(currency);
    }
}
