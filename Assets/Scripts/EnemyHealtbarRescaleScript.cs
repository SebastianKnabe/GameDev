using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealtbarRescaleScript : MonoBehaviour
{
    private float positionFloat = 0f;

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

        /* Aus irgendwelchen noch unbekannten Gründen ist der healthbarSprite nicht richtig positioniert
         * wenn man die Position anpassen will. Das ist erstmal ein Workaround damit es funktioniert.
         */

        Vector3 fixPosition = new Vector3(-13.41f, 23.40625f, 0);
        fixPosition.x += spriteWidth / 2f;

        GameObject child = gameObject.transform.GetChild(0).gameObject;
        child.transform.position = fixPosition;
        
    }

}
