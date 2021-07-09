using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abyss : MonoBehaviour
{
    [SerializeField] private VoidEvent onPlayerFellDown = null;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            onPlayerFellDown.Raise();
        }

        //TODO Enemey 
    }
}
