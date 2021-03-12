using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryCurrency : MonoBehaviour
{
    [SerializeField] private Inventory inventory = null;
    [SerializeField] private TextMeshProUGUI itemQuantityText = null;

    protected virtual void Start()
    {
        updateCurrencyUI();
    }

    private void OnEnable()
    {
        updateCurrencyUI();
    }

    public void updateCurrencyUI()
    {
        itemQuantityText.text =  inventory.ItemContainer.Currency.ToString();
    }
}
