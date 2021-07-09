using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealtbarRescaleScript : MonoBehaviour
{
    void Start()
    {
        rescaleHealthbar();
    }

   

    private void rescaleHealthbar()
    {
        GameObject parentEnemyObject = gameObject.transform.parent.gameObject;
        Vector3 healthbarPosition = parentEnemyObject.transform.position;
        float spriteWidth = parentEnemyObject.GetComponent<SpriteRenderer>().size.x * parentEnemyObject.transform.localScale.x;
        float spriteHeight = parentEnemyObject.GetComponent<SpriteRenderer>().size.y * parentEnemyObject.transform.localScale.y;

        healthbarPosition.x -= spriteWidth / 2f;
        healthbarPosition.y += spriteHeight * 0.75f;
        gameObject.transform.position = healthbarPosition;


        gameObject.GetComponentInChildren<SpriteRenderer>().size = new Vector2(spriteWidth, 0.2f);

    }

}
