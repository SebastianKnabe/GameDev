using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    [SerializeField] private float maxHitPoints;
    [SerializeField] private float collideDamage = 1f;
    [SerializeField] private GameObject enemyHealthbarPrefab; //Die EnemyEntity erstellt für sich selber eine Healthbar aus dem Prefab
    [SerializeField] private bool dropEnabled;
    [SerializeField] private bool isBoss;
    [SerializeField] private VoidEvent bossTookDamageEvent;

    private float currentHitPoints;
    private StateModel stateModel;
    private bool isShielded = false;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        currentHitPoints = maxHitPoints;
        stateModel = GetComponent<StateModel>();
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        circleCollider2D = gameObject.GetComponent<CircleCollider2D>();

        InitHealthbar();

        updateHealthbar();
    }

    public float getCurrentHitPoints()
    {
        return currentHitPoints;
    }

    /* 
     * Methode wird aufgerufen, wenn die Entity Schaden erleidet.
     */
    public void takeDamage(float incomingDamage)
    {
        if (stateModel != null)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerEntity>().takeDamage(collideDamage);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<CapsuleCollider2D>(), circleCollider2D);
        }
    }

    public void death()
    {
        if (animator != null)
        {
            animator.SetTrigger("isDead");
        }

        if (this.gameObject.GetComponent<EnemyShootingScript>())
        {
            this.gameObject.GetComponent<EnemyShootingScript>().enabled = false;
        }
        if (dropEnabled)
        {
            this.gameObject.GetComponent<DropLootScript>().dropLoot();
        }

        if (GetComponent<StateModel>() == null)
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
            enemyHealthbarPrefab.SetActive(false);
        }
        else
        {
            enemyHealthbarPrefab.SetActive(true);
            enemyHealthbarPrefab.transform.localScale = new Vector3(healthbarRatio, 1, 0);
        }
    }

    private void InitHealthbar()
    {
        if (!isBoss)
        {
            GameObject bar = Instantiate(enemyHealthbarPrefab);
            bar.transform.parent = gameObject.transform;
            enemyHealthbarPrefab = bar;
        }
    }


}
