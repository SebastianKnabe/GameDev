using System;
using UnityEngine;

[Serializable]
public class ItemContainer : IItemContainer
{
    private ItemSlot[] itemSlots = new ItemSlot[0];
    public Action OnItemsUpdate = delegate { };

    public ItemContainer(int size) => itemSlots = new ItemSlot[size];
    
    public ItemSlot GetSlotByIndex(int index) => itemSlots[index];
    public int ContainerSize() => itemSlots.Length;
    public int Currency { get; set; } = 0;

    /*
     * Item wird hinzugefügt, wenn genug Platz ist.
     */ 
    public ItemSlot AddItem(ItemSlot itemSlot)
    {
        Debug.Log("add item " + itemSlot.item.name);
        for(int i = 0; i < itemSlots.Length; i++)
        {
            if(itemSlots[i].item != null)
            {
                if(itemSlots[i].item == itemSlot.item)
                {
                    int slotRemainingSpace = itemSlots[i].item.MaxStack - itemSlots[i].quantity;

                    if(itemSlot.quantity <= slotRemainingSpace)
                    {
                        itemSlots[i].quantity += itemSlot.quantity;
                        itemSlot.quantity = 0;

                        OnItemsUpdate.Invoke();

                        return itemSlot;
                    }
                    else if (slotRemainingSpace > 0) 
                    {
                        itemSlots[i].quantity += slotRemainingSpace;
                        itemSlot.quantity -= slotRemainingSpace;
                    }
                }
            }
        }

        for(int i = 0; i < itemSlots.Length; i++)
        {
            if(itemSlots[i].item == null)
            {
                if(itemSlot.quantity <= itemSlot.item.MaxStack)
                {
                    itemSlots[i] = itemSlot;
                    itemSlot.quantity = 0;

                    OnItemsUpdate.Invoke();

                    return itemSlot;
                }
                else
                {
                    itemSlots[i] = new ItemSlot(itemSlot.item, itemSlot.item.MaxStack);
                    itemSlot.quantity -= itemSlot.item.MaxStack;
                }
            }
        }
        OnItemsUpdate.Invoke();

        return itemSlot;
    }

    public ItemSlot AddItemAtSlotIndex(ItemSlot itemSlot, int slotIndex)
    {
        Debug.Log(Log.logString(this.GetType(), "AddItemAtSlotIndex", "add Item at SlotIndex = " + slotIndex));
        if (itemSlots[slotIndex].item == itemSlot.item)
        {
            int slotRemainingSpace = itemSlots[slotIndex].item.MaxStack - itemSlots[slotIndex].quantity;

            if (itemSlot.quantity <= slotRemainingSpace)
            {
                itemSlots[slotIndex].quantity += itemSlot.quantity;
                itemSlot.quantity = 0;

                OnItemsUpdate.Invoke();
            }
            else if (slotRemainingSpace > 0)
            {
                itemSlots[slotIndex].quantity += slotRemainingSpace;
                itemSlot.quantity -= slotRemainingSpace;
            }
        }
        else if (itemSlots[slotIndex].item == null)
        {
            if (itemSlot.quantity <= itemSlot.item.MaxStack)
            {
                itemSlots[slotIndex] = itemSlot;
                itemSlot.quantity = 0;

                OnItemsUpdate.Invoke();

                return itemSlot;
            }
            else
            {
                itemSlots[slotIndex] = new ItemSlot(itemSlot.item, itemSlot.item.MaxStack);
                itemSlot.quantity -= itemSlot.item.MaxStack;
            }
        }
        return AddItem(itemSlot);
    }

    public int GetTotalQuantity(InventoryItem item)
    {
        int totalCount = 0;

        foreach(ItemSlot itemSlot in itemSlots)
        {
            if(itemSlot.item == null || itemSlot.item != item)
            {
                continue;
            }

            totalCount += itemSlot.quantity;
        }

        return totalCount;
    }

    public bool HasItem(InventoryItem item)
    {
        foreach(ItemSlot itemSlot in itemSlots)
        {
            if (itemSlot.item == null || itemSlot.item != item)
            {
                continue;
            }
            return true;
        }
        return false;
    }

    public void RemoveItem(ItemSlot itemSlot)
    {
        for(int i = 0; i < itemSlots.Length; i++)
        {
            if(itemSlots[i].item != null)
            {
                if(itemSlots[i].item == itemSlot.item)
                {
                    if(itemSlots[i].quantity < itemSlot.quantity)
                    {
                        itemSlot.quantity -= itemSlots[i].quantity;
                        itemSlots[i] = new ItemSlot();
                    }
                    else
                    {
                        itemSlots[i].quantity -= itemSlot.quantity;

                        if(itemSlots[i].quantity == 0)
                        {
                            itemSlots[i] = new ItemSlot();

                            OnItemsUpdate.Invoke();

                            return;
                        }
                    }
                }
            }
        }
    }

    //Removes Item at specfic Index. If Item is not removed it returns an empty ItemSlot
    public ItemSlot RemoveItemAtIndex(int slotIndex)
    {
        if(slotIndex < 0 || slotIndex > itemSlots.Length - 1)
        {
            return new ItemSlot();
        }
        ItemSlot removedItem = itemSlots[slotIndex];
        Debug.Log(removedItem.item.name + " " + removedItem.quantity);
        itemSlots[slotIndex] = new ItemSlot();
        OnItemsUpdate.Invoke();
        
        return removedItem;
    }

    public bool hasItems()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if(itemSlots[i].quantity > 0)
            {
                return true;
            }
        }
        return false;
    }

    /*
     * Swaps two Items.
     * If the Items are the same type they will be put together.
     */
    public void Swap(int indexOne, int indexTwo)
    {
        ItemSlot firstSlot = itemSlots[indexOne];
        ItemSlot secondSlot = itemSlots[indexTwo];

        Debug.Log("Swap");

        if(firstSlot == secondSlot)
        {
            return;
        }

        if(secondSlot != null)
        {
            if(firstSlot.item == secondSlot.item)
            {
                int secondSlotRemainingSpace = secondSlot.item.MaxStack - secondSlot.quantity;

                if(firstSlot.quantity <= secondSlotRemainingSpace)
                {
                    itemSlots[indexTwo].quantity += firstSlot.quantity;
                    itemSlots[indexOne] = new ItemSlot();

                    OnItemsUpdate.Invoke();

                    return;
                }
            }
        }

        itemSlots[indexOne] = secondSlot;
        itemSlots[indexTwo] = firstSlot;

        OnItemsUpdate.Invoke();
    }
}
