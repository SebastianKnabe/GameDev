using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthKitInfoUI : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private TextMeshProUGUI itemQuantityText;
    [SerializeField] private ConsumableItem healthKit;

    public void Start()
    {
        updateUI();
    }

    void OnEnable()
    {
        updateUI();
    }

    public void updateUI()
    {
        int quantity = inventory.ItemContainer.GetTotalQuantity(healthKit);
        itemQuantityText.text = quantity > 0 ? "" + quantity : "0";
    }
}
