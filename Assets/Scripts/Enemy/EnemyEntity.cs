using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    public float maxHitPoints;
    public GameObject healthbar;
    public bool dropEnabled;
    public bool isBoss;

    [SerializeField] private VoidEvent bossTookDamageEvent;

    private float currentHitPoints;
    private StateModel stateModel;
    private bool isShielded = false;
    private Animator animator;
    private SpriteRenderer spriteRenderer;


    public float getCurrentHitPoints()
    {
        return currentHitPoints;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHitPoints = maxHitPoints;
        stateModel = GetComponent<StateModel>();
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        InitHealthbar();

        updateHealthbar();
    }

    /* 
     * Methode wird aufgerufen, wenn die Entity Schaden erleidet.
     */
    public void takeDamage(float incomingDamage)
    {
        if(stateModel != null)
        {
            stateModel.fireDamageEvent();
        }
        if (!isShielded)
        {
            currentHitPoints -= incomingDamage;

            BossDamageEvent();

            if (currentHitPoints <= 0)
            {
                currentHitPoints = 0;
                updateHealthbar();
                death();
                return;
            }

            updateHealthbar();
        }
    }

    private void BossDamageEvent()
    {
        if (isBoss)
        {
            bossTookDamageEvent.Raise();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerEntity>().hitObstacle(50);
        }
    }

    public void death()
    {
        animator.SetTrigger("isDead");
        if (this.gameObject.GetComponent<EnemyShootingScript>())
        {
            this.gameObject.GetComponent<EnemyShootingScript>().enabled = false;
        }
        if (dropEnabled)
        {
            this.gameObject.GetComponent<DropLootScript>().dropLoot();
        }

        if(GetComponent<StateModel>() == null)
            Destroy(this.gameObject);
    }

    public float getHealthPercentage()
    {
        return currentHitPoints / maxHitPoints;
    }

    public void setShield(bool shield)
    {
        isShielded = shield;
        if (isShielded)
        {
            spriteRenderer.color = new Color32(0, 255, 255, 255);
        }
        else
        {
            spriteRenderer.color = new Color32(255, 255, 255, 255);
        }

    }

    private void updateHealthbar()
    {
        float healthbarRatio = currentHitPoints / maxHitPoints;
        if (healthbarRatio == 1f && !isBoss)
        {
            healthbar.SetActive(false);
        }
        else
        {
            healthbar.SetActive(true);
            healthbar.transform.localScale = new Vector3(healthbarRatio, 1, 0);
        }
    }

    private void InitHealthbar()
    {
        if (!isBoss)
        {
            GameObject bar = Instantiate(healthbar);
            bar.transform.parent = gameObject.transform;
            healthbar = bar;
        }
    }


}
