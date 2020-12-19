using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairMouseScript : MonoBehaviour
{
    public GameObject player;
    public GameObject bulletPrefab;
    public GameObject bulletStart;
    
    public float bulletSpeed = 60.0f;



    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false; 
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseCursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mouseCursor;
        Vector3 crosshairPlayerDifference = transform.position - bulletStart.transform.position;

        float rotationZ = Mathf.Atan2(crosshairPlayerDifference.y, crosshairPlayerDifference.x) * Mathf.Rad2Deg;

        if(Input.GetMouseButtonDown(0)){

            float distance = crosshairPlayerDifference.magnitude;
            Vector2 direction = crosshairPlayerDifference / distance;
            direction.Normalize();
            fireBullet(direction, rotationZ);

        }

    }

     void fireBullet(Vector2 direction, float rotationZ){
        GameObject b = Instantiate(bulletPrefab) as GameObject;
        b.transform.position = bulletStart.transform.position;
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        b.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

     
    }

}
