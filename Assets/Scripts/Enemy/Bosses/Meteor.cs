using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private float fallTimer = 0f;
    // Update is called once per frame
    void Update()
    {
        if (fallTimer > 10f)
        {
            Destroy(gameObject);
        }
        fallTimer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.gameObject != null)
        {
            collision.gameObject.GetComponent<PlayerEntity>().takeDamage(damage);
        }
    }
}
