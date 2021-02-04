using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Items/Inventory")]
public class Inventory : ScriptableObject
{
    [SerializeField] private VoidEvent onInventoryItemsUpdated = null;
    [SerializeField] private ItemSlot testItemSlot = new ItemSlot();

    public ItemContainer ItemContainer { get; } = new ItemContainer(20);

    public void OnEnable() => ItemContainer.OnItemsUpdate += onInventoryItemsUpdated.Raise;

    public void OnDisable() => ItemContainer.OnItemsUpdate -= onInventoryItemsUpdated.Raise;

    [ContextMenu("Test Add")]
    public void TestAdd()
    {
        ItemContainer.AddItem(testItemSlot);
    }

    public void addCurrency(int addCurrency)
    {
        ItemContainer.Currency += addCurrency;
    }
}
