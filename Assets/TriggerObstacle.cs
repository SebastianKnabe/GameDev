using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObstacle : MonoBehaviour
{

    public float bulletSpeed = 60.0f;

    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            coroutine = StartShooting(new Vector2(-1, 0), 0);
            StartCoroutine(coroutine);
        }
    }


    IEnumerator StartShooting(Vector2 direction, float rotationZ)
    {
        gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        gameObject.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

        yield return new WaitForSeconds(1f);
    }
}
