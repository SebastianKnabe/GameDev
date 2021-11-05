﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{

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

        if (other.tag == "Player")
        {
            Destroy(this.gameObject);
            ScreenShakeController.instace.startShake(.5f, .25f);
            other.gameObject.GetComponent<PlayerEntity>().takeDamage(damage);

        }
        else if (other.tag == "Ground")
        {
            ScreenShakeController.instace.startShake(.5f, .25f);
            Destroy(this.gameObject);
        }
    }
}
