using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Item", menuName = "Items/Quest Item")]
public class QuestItem : InventoryItem
{
    [Header("Quest Data")]
    [SerializeField] private string questText = "Import for something.";

    public override string GetInfoDisplayText()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("<color=yellow>").Append(questText).Append("</color>").AppendLine();

        return builder.ToString();
    }
}
