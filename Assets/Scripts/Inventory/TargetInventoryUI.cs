using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetInventoryUI : MonoBehaviour
{
    [SerializeField] Inventory targetInventory = null;
    [SerializeField] private TextMeshProUGUI targetInventoryLabel = null;


    private void OnEnable()
    {
        targetInventoryLabel.text  = targetInventory.inventoryType.ToString();
    }
}
