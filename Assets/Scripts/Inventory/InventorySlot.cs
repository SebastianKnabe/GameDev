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
            InventorySlot targetSlot = itemDragHandler.ItemSlotUI as InventorySlot;

            if(targetSlot.inventory != inventory)
            {
                Debug.Log("onDrop: on different inventory");
                tradeItems(targetSlot, itemDragHandler);

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

    private void tradeItems(InventorySlot targetSlot, ItemDragHandler itemDragHandler)
    {
        ItemSlot swapItem = targetSlot.inventory.ItemContainer.getItemAtIndex(itemDragHandler.ItemSlotUI.SlotIndex);

        if (inventory.inventoryType == InventoryType.Vendor || targetSlot.inventory.inventoryType == InventoryType.Vendor)
        {            
            int buyCount = inventory.ItemContainer.Currency / swapItem.item.SellPrice;
            int price = swapItem.item.SellPrice * swapItem.quantity;

            if(buyCount < swapItem.quantity)
            {
                price = swapItem.item.SellPrice * buyCount;
                swapItem.quantity -= buyCount;

                targetSlot.inventory.ItemContainer.RemoveItem(swapItem);
                inventory.ItemContainer.AddItemAtSlotIndex(swapItem, SlotIndex);
            }
            else
            {
                targetSlot.inventory.ItemContainer.RemoveItem(swapItem);
            }
            targetSlot.inventory.ItemContainer.addCurrency(price);
            inventory.ItemContainer.addCurrency(-1 * price);
        } else
        {
            targetSlot.inventory.ItemContainer.RemoveItem(swapItem);
            inventory.ItemContainer.AddItemAtSlotIndex(swapItem, SlotIndex);
        }
    }
}
