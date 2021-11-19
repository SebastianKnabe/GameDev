using UnityEngine;
using System.Collections;

public class DispenserMunitionDamagePlayer : MonoBehaviour
{
    public float damageTaken;
    public bool resetPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (resetPlayer)
            {
                collision.GetComponent<PlayerEntity>().hitObstacle(damageTaken);
            }
            else
            {
                collision.GetComponent<PlayerEntity>().takeDamage(damageTaken);
            }

        }
    }
}
