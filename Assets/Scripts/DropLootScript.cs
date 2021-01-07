using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Loot Script fuer Gegner.
 *  In einer Liste sind die GameObjects die gedroppt werden. 
 *  In der anderen Liste sind die Gewichtungen der Items.
 *  Wenn Nichts fallen gelassen werden soll, kann ein leeres GameObject mit Gewichtung eingefügt werden.
 */
public class DropLootScript : MonoBehaviour
{
    public List<GameObject> lootItem;
    public List<float> dropWeights;
    public Dictionary<string, string> test;

    private float maxLootWeight = 0;
    private string TAG = "DropLootScript: ";
    private Transform spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = gameObject.transform;
        //Falls Items keine Lootchance haben, wird default value hinzugefuegt.
        while(lootItem.Count > dropWeights.Count)
        {
            Debug.Log(TAG + "Items haben keine Lootchance! Füge Default Value hinzu");
            dropWeights.Add(1f);
        }

        //berechnet die maximale Loot Range. So kann man Loot einfacher gewichten.
        for(int i = 0; i < dropWeights.Count;i++)
        {
            maxLootWeight += dropWeights[i];
        }

    }

    public void dropLoot()
    {
        float randomNumber = Random.Range(0, maxLootWeight);

        float lootChance = 0f;
        Debug.Log(TAG + "randomNumber = " + randomNumber);
        for(int i = 0; i < lootItem.Count;i++)
        {
            lootChance += dropWeights[i];
            if(randomNumber < lootChance)
            {
                GameObject dropItem = lootItem[i];
                if(dropItem != null)
                {
                    Instantiate(lootItem[i], spawnPosition.position, spawnPosition.rotation);
                }
            }
        }
    }
}
