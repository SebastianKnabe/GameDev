using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    public float maxHitPoints;
    public GameObject healthbar;

    private float currentHitPoints;
    public bool dropEnabled;

    // Start is called before the first frame update
    void Start()
    {
        currentHitPoints = maxHitPoints;
        
        updateHealthbar();
    }

    /* Methode wird aufgerufen, wenn die Entity Schaden erleidet.
     * 
     */
    public void takeDamage(float incomingDamage)
    {
        currentHitPoints -= incomingDamage;
        
        if(currentHitPoints <= 0)
        {
            death();
            return;
        }

        updateHealthbar();
    }

    public void death()
    {
        //TODO
        if(dropEnabled){
            gameObject.GetComponent<DropLootScript>().dropLoot();
        }
        Destroy(this.gameObject);
    }

    private void updateHealthbar()
    {
        float healthbarRatio = currentHitPoints / maxHitPoints;
        healthbar.transform.localScale = new Vector3(healthbarRatio, 1, 0);
    }
}
