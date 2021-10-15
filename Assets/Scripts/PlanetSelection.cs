using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetSelection : MonoBehaviour
{
    [SerializeField] private int planetScene;

    public void selectPlanet()
    {
        if (planetScene > SceneManager.GetActiveScene().buildIndex)
        {
            SceneManager.LoadScene(planetScene);
        }
    }
}
