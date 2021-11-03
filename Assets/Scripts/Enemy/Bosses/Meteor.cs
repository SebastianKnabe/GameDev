using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    private IEnumerator coroutine;

    private void Start()
    {
        coroutine = DestroyMeteor();
        StartCoroutine(coroutine);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.gameObject != null)
        {
            collision.gameObject.GetComponent<PlayerEntity>().takeDamage(damage);
        }
    }

    private IEnumerator DestroyMeteor()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);

    }



}
