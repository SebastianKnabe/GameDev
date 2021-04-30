using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    public float maxHitPoints;
    public GameObject healthbar;
    public bool dropEnabled;

    private float currentHitPoints;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentHitPoints = maxHitPoints;
        animator = gameObject.GetComponent<Animator>();

        updateHealthbar();
    }

    /* Methode wird aufgerufen, wenn die Entity Schaden erleidet.
     * 
     */
    public void takeDamage(float incomingDamage)
    {
        currentHitPoints -= incomingDamage;

        if (currentHitPoints <= 0)
        {
            death();
            return;
        }

        updateHealthbar();
    }

    public void death()
    {
        //TODO
        animator.SetTrigger("isDead");
        if (dropEnabled)
        {
            this.gameObject.GetComponent<DropLootScript>().dropLoot();
        }
    }

    private void updateHealthbar()
    {
        float healthbarRatio = currentHitPoints / maxHitPoints;
        if (healthbarRatio == 1f)
        {
            healthbar.SetActive(false);
        }
        else
        {
            healthbar.SetActive(true);
            healthbar.transform.localScale = new Vector3(healthbarRatio, 1, 0);
        }

    }
}
