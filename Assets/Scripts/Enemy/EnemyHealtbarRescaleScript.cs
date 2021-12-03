using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealtbarRescaleScript : MonoBehaviour
{
    [ContextMenu("Rescale")]
    public void rescaleHealthbar()
    {
        GameObject parentEnemyObject = gameObject.transform.parent.gameObject;
        Vector3 healthbarPosition = parentEnemyObject.transform.position;

        float parentRotation = parentEnemyObject.transform.rotation.z;
        float spriteWidth = parentEnemyObject.GetComponent<SpriteRenderer>().size.x * parentEnemyObject.transform.localScale.x;
        float spriteHeight = parentEnemyObject.GetComponent<SpriteRenderer>().size.y * parentEnemyObject.transform.localScale.y;

        if (spriteWidth < 1f)
        {
            spriteWidth *= 10;
            spriteHeight *= 10;
        }

        healthbarPosition.x -= spriteWidth / 2f;
        healthbarPosition.y += spriteHeight * 0.75f;
        gameObject.transform.position = healthbarPosition;

        gameObject.GetComponentInChildren<SpriteRenderer>().size = new Vector2(spriteWidth, 0.2f);
    }

}
