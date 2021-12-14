using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycasttest : MonoBehaviour
{
    public Transform bulletStart;
    public int rayCount = 1;
    private CrosshairMouseScript crosshairScript; 
    public GameObject crosshair;
    public LayerMask layerMask;
    void Start()
    {
        crosshairScript = crosshair.GetComponent<CrosshairMouseScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 crosshairPlayerDifference = crosshairScript.getCrosshairPlayerPosition();
        float distance = crosshairPlayerDifference.magnitude;
        float rotationZ = Mathf.Atan2(crosshairPlayerDifference.y, crosshairPlayerDifference.x) * Mathf.Rad2Deg;
        Vector3 direction = crosshairPlayerDifference / distance;
        direction.Normalize();

        CastRay(bulletStart.position, direction);
        
    }

    private void CastRay(Vector3 position, Vector3 direction)
    {

        for (int i = 0; i < rayCount; i++)
        {
            Ray ray = new Ray(position, direction);
            RaycastHit2D[] hits = Physics2D.LinecastAll(position, position + direction*100);
            bool collided = false;
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    Debug.DrawLine(position, hit.point, Color.red);
                    position = hit.point;
                    int offsetX, offsetY = 0;

                    offsetX = direction.x < 0 ? 1 : -1;
                    Debug.Log(direction);
                    //offsetY = direction.y < 0 ? 1 : -1;
                    direction = Vector3.Reflect(direction, new Vector2(hit.normal.x + offsetX , hit.normal.y ));

                    collided = true;
                    break;
                }

            }

        }
        Debug.DrawRay(position, direction * 10, Color.blue);
    }
}
/*

for (int i = 0; i < rayCount; i++)
{
    Ray ray = new Ray(position, direction);
    RaycastHit2D[] hits = Physics2D.RaycastAll(position, direction);
    bool collided = false;
    foreach (RaycastHit2D hit in hits)
    {
        if (hit.collider.CompareTag("Ground"))
        {
            Debug.DrawLine(position, hit.point, Color.red);
            position = hit.point;
            int offsetX, offsetY = 0;

            offsetX = direction.x < 0 ? 5 : -5;
            Debug.Log(offsetX);
            //offsetY = direction.y < 0 ? 1 : -1;
            direction = Vector3.Reflect(direction, new Vector2(hit.normal.x, hit.normal.y + offsetY));

            collided = true;
            break;
        }

    }

}
*/