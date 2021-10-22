using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetSelection : MonoBehaviour
{
    [SerializeField] private int planetScene;
    [SerializeField] private string planetName;
    [SerializeField] private string flavourText;

    public void selectPlanet()
    {
        if (planetScene > SceneManager.GetActiveScene().buildIndex)
        {
            SceneManager.LoadScene(planetScene);
        }
    }

    public string getPlanetName()
    {
        return planetName;
    }

    public string getFlavourText()
    {
        return flavourText;
    }
}
