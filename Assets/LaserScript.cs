using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    // Start is called before the first frame update
    private LineRenderer lineRender;
    public Transform start;
    public GameObject crosshair;
    private CrosshairMouseScript crosshairScript;
    public SpriteManager spriteManager;
    void Start()
    {
        lineRender = GetComponent<LineRenderer>();
        crosshairScript = crosshair.GetComponent<CrosshairMouseScript>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 crosshairPlayerDifference = crosshairScript.getCrosshairPlayerPosition();
        float distance = crosshairPlayerDifference.magnitude;
        float rotationZ = Mathf.Atan2(crosshairPlayerDifference.y, crosshairPlayerDifference.x) * Mathf.Rad2Deg;

        Vector3 direction = crosshairPlayerDifference / distance;
        direction.Normalize();
        lineRender.SetPosition(0, start.transform.position);
        RaycastHit2D hit = Physics2D.Raycast(start.transform.position,  direction * 20);
        if (hit.collider && !hit.collider.gameObject.CompareTag("Bounds"))
        {
            Debug.Log(hit.collider.gameObject.tag);
            lineRender.SetPosition(1,  new Vector3(hit.point.x, hit.point.y, start.transform.position.z));
        }
        else if(getValidRange(rotationZ))
        {
            lineRender.SetPosition(1, start.transform.position + ( direction * 20));
        }


    }

    public bool getValidRange(float rotationZ)
    {
        if((spriteManager.getFacingRight() && rotationZ <= 90 && rotationZ >= -90) || (!spriteManager.getFacingRight() && (rotationZ >= 90 || rotationZ <= -90)))
        {
           return true;
        }
        return false;
    }


}
