using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TorchFlicker : MonoBehaviour
{

    private Light2D light;
    public float flickerTimer = 0.2f;
    public float rangeFrom = 1.3f;
    public float rangeTo = 3f;

    private void Start()
    {
        light = GetComponent<Light2D>();
        InvokeRepeating("flicker", 0.1f, flickerTimer);

    }

    private void flicker()
    {
        light.intensity = Random.Range(rangeFrom, rangeTo);
    }
}
