using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour
{
    [SerializeField] private GameObject toggleObject;

    public void EnableObject()
    {
        toggleObject.SetActive(true);
    }

    public void DisableObject()
    {
        toggleObject.SetActive(false);
    }
}
