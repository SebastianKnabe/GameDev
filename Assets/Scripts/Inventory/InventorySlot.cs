using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : ItemSlotUI, IDropHandler
{
    [SerializeField] private Inventory inventory = null;
    [SerializeField] private TextMeshProUGUI itemQuantityText = null;

    public override HotbarItem SlotItem
    {
        get { return ItemSlot.item; }
        set { }
    }

    public ItemSlot ItemSlot => inventory.ItemContainer.GetSlotByIndex(SlotIndex);

    public override void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop: " + eventData.pointerDrag.GetComponent<ItemDragHandler>());
        
        ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();
        
        if(itemDragHandler == null)
        {
            Debug.Log("OnDrop: Return");
            return;
        }

        if((itemDragHandler.ItemSlotUI as InventorySlot) != null)
        {
            Debug.Log(Log.logString(this.GetType(), "OnDrop", "Swap"));
            InventorySlot targetSlot = itemDragHandler.ItemSlotUI as InventorySlot;

            if(targetSlot.inventory != inventory)
            {
                Debug.Log("onDrop: on different inventory");
                ItemSlot swapItem = targetSlot.inventory.ItemContainer.RemoveItemAtIndex(itemDragHandler.ItemSlotUI.SlotIndex);
                inventory.ItemContainer.AddItemAtSlotIndex(swapItem, SlotIndex);

                UpdateSlotUI();
            }
            else
            {
                inventory.ItemContainer.Swap(itemDragHandler.ItemSlotUI.SlotIndex, SlotIndex);
            }
        }
    }

    public override void UpdateSlotUI()
    {
        if(ItemSlot.item == null)
        {
            EnableSlotUI(false);
            return;
        }

        EnableSlotUI(true);

        itemIconImage.sprite = ItemSlot.item.Icon;
        itemQuantityText.text = ItemSlot.quantity > 1 ? ItemSlot.quantity.ToString() : "";
    }

    protected override void EnableSlotUI(bool enable)
    {
        base.EnableSlotUI(enable);
        itemQuantityText.enabled = enable;
    }
}
