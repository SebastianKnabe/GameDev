using UnityEngine;

public class BulletScript : MonoBehaviour
{
    //public float bulletSpeed = 60.0f;
    public float damage = 3.0f;
    public float bulletSpeed = 60.0f;
    public float weaponCooldown = 1f;


    public string shooter = "Enemy";
    [SerializeField] protected bool isDestructable = false;
    private Rigidbody2D rb;
    private Vector2 screenBounds;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.GetComponent<Renderer>().isVisible)
        {
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        //Wenn Kugel vom gleichen Tag ist, passiert nichts
        //Mögliche Tags der Kugel sind Player oder Enemy
        Debug.Log(other.gameObject.tag);
        Debug.Log("??? " + gameObject.tag);
        if (other.gameObject.tag == "Untagged" || other.gameObject.tag == shooter)
        {
            return;
        }
        else
        {
            BulletScript targetBulletScript = other.gameObject.GetComponent<BulletScript>();
            if (targetBulletScript != null)
            {
                if (targetBulletScript.isDestructable)
                {
                    Destroy(this.gameObject);
                    Destroy(other.gameObject);
                }
            }
        }
        Debug.Log("Tag: " + other.gameObject.tag + " from " + other.gameObject.name);
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyEntity>().takeDamage(damage);
            ScreenShakeController.instace.startShake(.5f, 0.2f);
            Destroy(this.gameObject);
            
        }
        else if (other.gameObject.tag == "Player")
        {
            PlayerEntity targetEntity = other.gameObject.GetComponent<PlayerEntity>();
            if (targetEntity != null)
            {
                other.gameObject.GetComponent<PlayerEntity>().takeDamage(damage);
                Destroy(this.gameObject);
            }
        }
        else if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Wall"))
        {
            ScreenShakeController.instace.startShake(.5f, 0.2f);
            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "Destructable")
        {
            Debug.Log("Hello?");
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

    }


}
