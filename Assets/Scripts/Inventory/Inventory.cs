using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Items/Inventory")]
public class Inventory : ScriptableObject
{
    [SerializeField] private VoidEvent onInventoryItemsUpdated = null;

    public InventoryType inventoryType = InventoryType.Container;
    public ItemContainer ItemContainer { get; set; } = new ItemContainer(20);

    public void OnEnable() => ItemContainer.OnItemsUpdate += onInventoryItemsUpdated.Raise;

    public void OnDisable() => ItemContainer.OnItemsUpdate -= onInventoryItemsUpdated.Raise;

    public void addCurrency(int addCurrency)
    {
        ItemContainer.Currency += addCurrency;
    }

    public void setOnInventoryItemsUpdatedEvent(VoidEvent onInventoryItemsUpdatedEvent) {
        onInventoryItemsUpdated = onInventoryItemsUpdatedEvent;
    }

    [ContextMenu("Reset Inventory")]
    public void ResetInventory()
    {
        ItemContainer = new ItemContainer(20);
    }
}
