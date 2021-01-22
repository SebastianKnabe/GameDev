using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : MonoBehaviour
{
    public float maxHitPoints;
    public GameObject healthbar;
    public SpriteRenderer spriteRenderer; 

    private float currentHitPoints;
    private float hitRate = 1.5f;
    private float timeSinceLastDamage;
    private bool damageCooldown;
    
    private float timeSinceLastColorChange;
    private float changeColorHitRate;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentHitPoints = maxHitPoints;
        damageCooldown = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        changeColorHitRate = 0.3f;
        updateHealthbar();
        animator = GetComponent<Animator>();
    }

    void Update(){
        
    if(damageCooldown){
            //spriteRenderer.color = new Color32(255,0,0,255);
            if(Time.time >  timeSinceLastColorChange + changeColorHitRate){
                spriteRenderer.color = spriteRenderer.color == new Color32(255,0,0,255) ? new Color32(255,255,255,255) : new Color32(255,0,0,255);
                timeSinceLastColorChange = Time.time;
            }
            damageCooldown = timeSinceLastDamage + hitRate > Time.time;

            if(!damageCooldown){
                spriteRenderer.color =  new Color32(255,255,255,255);
            }
        }

    }

    /* Methode wird aufgerufen, wenn die Entity Schaden erleidet.
     * 
     */
    public void takeDamage(float incomingDamage)
    {
        if(damageCooldown){
            return;
        }
        animator.SetTrigger("takingDamage");
        currentHitPoints -= incomingDamage;
        
        if(currentHitPoints <= 0)
        {
            death();
            return;
        }

        updateHealthbar();

        damageCooldown = true;
        timeSinceLastDamage = Time.time;
        timeSinceLastColorChange = Time.time - changeColorHitRate;

    }

    public void death()
    {
        //TODO
       
        Destroy(this.gameObject);
    }

    private void updateHealthbar()
    {
        float healthbarRatio = currentHitPoints / maxHitPoints;
        healthbar.transform.localScale = new Vector3(healthbarRatio, 1, 0);
    }
}
