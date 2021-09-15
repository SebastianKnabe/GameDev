using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private GameObject camera;
    [SerializeField] private float paralaxxEffect;

    private float length;
    private float startposX;

    void Start()
    {
        startposX = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float temp = (camera.transform.position.x * (1 - paralaxxEffect));
        float distance = camera.transform.position.x * paralaxxEffect;

        transform.position = new Vector3(startposX + distance, transform.position.y, transform.position.z);

        if (temp > startposX + length * 0.75f)
        {
            startposX += length;
        }
        else if (temp < startposX - length * 0.75f)
        {
            startposX -= length;
        }
    }
}
