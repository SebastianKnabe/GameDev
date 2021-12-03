using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateTeleportsScript : MonoBehaviour
{

    [SerializeField] private GameObject tpFrom;
    [SerializeField] private GameObject tpTo;

    public void ActivateTeleports()
    {
        tpFrom.SetActive(true);
        tpTo.SetActive(true);
    }
}
