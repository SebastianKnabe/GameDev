using UnityEngine;
using System.Collections;

public class DispenserMunitionDamagePlayer : MonoBehaviour
{

    public float damageTaken;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            collision.GetComponent<PlayerEntity>().hitObstacle(damageTaken);

        }
    }
}
