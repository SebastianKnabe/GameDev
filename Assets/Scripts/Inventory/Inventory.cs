using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Items/Inventory")]
public class Inventory : ScriptableObject
{
    [SerializeField] private VoidEvent onInventoryItemsUpdated = null;

    public ItemContainer ItemContainer { get; } = new ItemContainer(20);

    public void OnEnable() => ItemContainer.OnItemsUpdate += onInventoryItemsUpdated.Raise;

    public void OnDisable() => ItemContainer.OnItemsUpdate -= onInventoryItemsUpdated.Raise;

    public void addCurrency(int addCurrency)
    {
        ItemContainer.Currency += addCurrency;
    }
}
