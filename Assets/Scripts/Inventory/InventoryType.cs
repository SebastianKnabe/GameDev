using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Enum for inventory types
 * Player: Inventory of the player
 * Container: inventory of lootable Containers like chests
 *            its not allowed to trade back into containers
 * Vendor: inventory of a vendor
 *         items can be sold or bought
 */ 
public enum InventoryType
{
    Player,
    Container,
    Vendor
    //Bank
}
