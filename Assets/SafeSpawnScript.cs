﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeSpawnScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerMovement>().setSafeSpawn(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerMovement>().setSafeSpawn(true);
        }
    }
}