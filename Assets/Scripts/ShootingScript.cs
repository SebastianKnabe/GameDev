using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{

    public GameObject crosshair;
    public GameObject bulletPrefab;
    public GameObject bulletStart;
    public float bulletSpeed = 60.0f;

    private Animator animator;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    

    // Update is called once per frame
    void Update()
    {
    
       Vector3 crosshairPlayerDifference = crosshair.GetComponent<CrosshairMouseScript>().getCrosshairPlayerPosition();
       float rotationZ = Mathf.Atan2(crosshairPlayerDifference.y, crosshairPlayerDifference.x) * Mathf.Rad2Deg;

       if(Input.GetMouseButtonDown(0)){

            float distance = crosshairPlayerDifference.magnitude;
            Vector2 direction = crosshairPlayerDifference / distance;
            direction.Normalize();
            fireBullet(direction, rotationZ);
            animator.SetTrigger("shooting");
        }
    }

       void fireBullet(Vector2 direction, float rotationZ){
        GameObject b = Instantiate(bulletPrefab) as GameObject;
        b.transform.position = bulletStart.transform.position;
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        b.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

     
    }
}
