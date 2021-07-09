using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.localPosition.y < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.gameObject != null)
        {
            collision.gameObject.GetComponent<PlayerEntity>().takeDamage(damage);
        }
    }
}
