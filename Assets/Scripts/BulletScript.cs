﻿using UnityEngine;

public class BulletScript : MonoBehaviour
{

    //public float bulletSpeed = 60.0f;
    public float damage = 3.0f;
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
        Debug.Log(other.tag);
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyEntity>().takeDamage(damage);
            Destroy(this.gameObject);
            
        }
        else if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        } else if (other.tag == "Destructable")
        {
            Debug.Log("Hello?");
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

    }


}
