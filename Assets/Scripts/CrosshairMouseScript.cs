using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairMouseScript : MonoBehaviour
{
    public GameObject bulletStart;
    private Vector3 crosshairPlayerDifference; 


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false; 
        crosshairPlayerDifference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - bulletStart.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        crosshairPlayerDifference = transform.position - bulletStart.transform.position;

       

    }

    public Vector2 getCrosshairPlayerPosition(){
            return crosshairPlayerDifference;

    }

}
