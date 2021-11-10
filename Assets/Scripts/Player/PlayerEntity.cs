using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerEntity : MonoBehaviour
{
    //public static PlayerEntity instance;

    public float maxHitPoints;
    public GameObject healthbar;
    public SpriteRenderer spriteRenderer;
    public Scene scene;
    public Inventory inventory;
    public InventoryItem healthKit;
    public AudioSource audioSource;
    public Transform spawnPoint;
    public Camera camera;

    //private string spawnPoint;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip respawnSound;
    private float currentHitPoints;
    private float hitRate = 1.5f;
    private float timeSinceLastDamage;
    private bool damageCooldown;
    private Rigidbody2D rb;

    private float timeSinceLastColorChange;
    private float changeColorHitRate;
    private Animator animator;


    /*
    public void setSpawnPoint(string spawnPoint)
    {
        this.spawnPoint = spawnPoint;
    }

    public string getSpawnPoint()
    {
        return this.spawnPoint;
    }
    /*/
    // Start is called before the first frame update
    void Start()
    {
        /*
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        /*/
        rb = gameObject.GetComponent<Rigidbody2D>();
        currentHitPoints = maxHitPoints;
        damageCooldown = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        changeColorHitRate = 0.3f;
        updateHealthbar();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (damageCooldown)
        {
            //spriteRenderer.color = new Color32(255,0,0,255);
            if (Time.time > timeSinceLastColorChange + changeColorHitRate)
            {
                spriteRenderer.color = spriteRenderer.color == new Color32(255, 0, 0, 255) ? new Color32(255, 255, 255, 255) : new Color32(255, 0, 0, 255);
                timeSinceLastColorChange = Time.time;
            }
            damageCooldown = timeSinceLastDamage + hitRate > Time.time;

            if (!damageCooldown)
            {
                spriteRenderer.color = new Color32(255, 255, 255, 255);
            }
        }

        if (Input.GetKeyUp(KeyCode.H) && (currentHitPoints != maxHitPoints))
        {
            useHealthKit();
        }
    }

    private void useHealthKit()
    {
        if (inventory.ItemContainer.HasItem(healthKit))
        {
            currentHitPoints += maxHitPoints * 0.25f;
            if (currentHitPoints > maxHitPoints)
            {
                currentHitPoints = maxHitPoints;
            }
            updateHealthbar();

            ItemSlot removeItem = new ItemSlot(healthKit, 1);
            inventory.ItemContainer.RemoveItem(removeItem);
        }
    }

    /* Methode wird aufgerufen, wenn die Entity Schaden erleidet.
     * 
     */
    public void takeDamage(float incomingDamage)
    {
        if (damageCooldown)
        {
            return;
        }
        animator.SetTrigger("takingDamage");
        currentHitPoints -= incomingDamage;

        if (currentHitPoints <= 0)
        {
            death();
            return;
        }

        updateHealthbar();

        damageCooldown = true;
        timeSinceLastDamage = Time.time;
        timeSinceLastColorChange = Time.time - changeColorHitRate;

    }

    public void hitObstacle(float damageTaken)
    {
        rb.AddForce(transform.up * 15, ForceMode2D.Impulse);
        takeDamage(damageTaken);
        StartCoroutine("WaitForHit");
    }

    IEnumerator WaitForHit()
    {
        yield return new WaitForSeconds(0.5f);
        if (currentHitPoints > 0)
        {
            this.transform.position = spawnPoint.position;
            camera.gameObject.GetComponent<CameraFollow>().MoveCameraToSpawnPoint(spawnPoint);
        }
    }


    IEnumerator Respawn()
    {
        // set to 0.5 since we have no death animation, was at 2
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(respawnSound);
        currentHitPoints = 400;
        updateHealthbar();
        this.transform.position = spawnPoint.position;
        camera.gameObject.GetComponent<CameraFollow>().MoveCameraToSpawnPoint(spawnPoint);
    }

    public void takeDamageFromEffect(float incomingDamage)
    {
        animator.SetTrigger("takingDamage");
        currentHitPoints -= incomingDamage;

        if (currentHitPoints <= 0)
        {
            death();
            return;
        }

        updateHealthbar();
        //timeSinceLastColorChange = Time.time - changeColorHitRate;
    }

    public void death()
    {
        //TODO
        updateHealthbar();
        audioSource.PlayOneShot(deathSound);
        StartCoroutine("Respawn");

    }

    private void updateHealthbar()
    {
        float healthbarRatio = currentHitPoints / maxHitPoints;
        healthbar.transform.localScale = new Vector3(healthbarRatio, 1, 0);
    }

    public void SetNextSpawn(Vector3 newSpawn)
    {
        spawnPoint.position = newSpawn;
    }
}
