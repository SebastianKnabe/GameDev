using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private GameObject camera;
    [SerializeField] private float paralaxxEffect;
    public bool useYAxis = false; 

    private float length;
    private float height;
    private float startposX;
    private float startposY;

    void Start()
    {
        startposX = transform.position.x;
        startposY = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        float tempX = (camera.transform.position.x * (1 - paralaxxEffect));
        float distanceX = camera.transform.position.x * paralaxxEffect;
        float tempY = (camera.transform.position.y * (1 - paralaxxEffect));
        float distanceY = (camera.transform.position.y * (1 - paralaxxEffect));

        transform.position = new Vector3(startposX + distanceX, useYAxis ? startposY + distanceY : startposY, transform.position.z);

        if (tempX > startposX + length * 0.75f)
        {
            startposX += length;
        }
        else if (tempX < startposX - length * 0.75f)
        {
            startposX -= length;
        } else if (useYAxis && tempY < startposY - height * 0.75f)
        {
            startposY -= height;
        } else if (useYAxis && tempY > startposY + height * 0.75f)
        {
            startposY += height;
        }
    }
}
