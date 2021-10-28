using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TorchFlicker : MonoBehaviour
{

    private Light2D light;

    private void Start()
    {
        light = GetComponent<Light2D>();
        InvokeRepeating("flicker", 1f, 1f);

    }

    private void flicker()
    {
        light.intensity = Random.Range(0.5f, 2.9f);
    }
}
