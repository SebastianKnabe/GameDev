using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeSpawnScript : MonoBehaviour
{
    [Header("Safe")]
    [SerializeField] private VoidEvent playerSpawnSafe;

    [Header("Unsafe")]
    [SerializeField] private VoidEvent playerSpawnUnsafe;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerSpawnUnsafe.Raise();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerSpawnSafe.Raise();
        }
    }
}
