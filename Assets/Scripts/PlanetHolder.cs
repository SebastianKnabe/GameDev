using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetHolder : MonoBehaviour
{
    [SerializeField] [Range(1f, 5f)] private float scaleTimeFactor = 2.5f;

    private int currentSelectedPlanet;
    private int numberOfPlanets;
    private int maxPlanetDistance;
    private int[] planetPosition;

    // Start is called before the first frame update
    void Start()
    {
        currentSelectedPlanet = 0;
        numberOfPlanets = gameObject.transform.childCount;
        maxPlanetDistance = numberOfPlanets / 2;
        planetPosition = new int[numberOfPlanets];

        SetPlanetPositions();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            currentSelectedPlanet -= 1;
            if (currentSelectedPlanet < 0)
            {
                currentSelectedPlanet = numberOfPlanets - 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            currentSelectedPlanet += 1;
            if (currentSelectedPlanet >= numberOfPlanets)
            {
                currentSelectedPlanet = 0;
            }
        }
        SetPlanetPositions();
    }

    private void SetPlanetPositions()
    {
        for (int i = 0; i < numberOfPlanets; i++)
        {
            //Bestimme Planeten Position im Kreis
            int planetArrayPosition = i - currentSelectedPlanet;

            if (planetArrayPosition > maxPlanetDistance)
            {
                //Debug.Log("i: " + i + "\nplanetarrayPosition: " + planetArrayPosition);
                planetArrayPosition = -3 + planetArrayPosition - maxPlanetDistance;
            }
            else if (planetArrayPosition < -maxPlanetDistance)
            {
                //Debug.Log("i: " + i + "\nplanetarrayPosition: " + planetArrayPosition);
                planetArrayPosition = 3 - Mathf.Abs(planetArrayPosition) + maxPlanetDistance;
            }

            //Ermittle Position
            float xPosition = planetArrayPosition * 400;
            float yPosition = Mathf.Abs(planetArrayPosition) * 2 * 100f;
            if (Mathf.Abs(planetArrayPosition) == maxPlanetDistance)
            {
                xPosition = 60 * planetArrayPosition;
                yPosition = 400;
            }

            //Für Debug falls Planeten Anzahl > 5
            planetPosition[i] = planetArrayPosition;

            //Planet-Gameobject
            GameObject planet = gameObject.transform.GetChild(i).gameObject;

            //Bewege zu Position
            Vector3 targetPosition = new Vector3(xPosition, yPosition, 0f);
            Vector3 currentPosition = planet.transform.localPosition;
            float positionDistance = (currentPosition - targetPosition).magnitude;
            planet.transform.localPosition = Vector3.MoveTowards(currentPosition, targetPosition, positionDistance / 100f);

            //Skaliere Planeten -> Vordergrund größer, Hintergrund kleiner
            Vector3 currentScale = planet.transform.localScale;
            if (planetArrayPosition == 0)
            {
                Vector3 targetScale = new Vector3(1.5f, 1.5f, 1.5f);
                planet.transform.localScale = Vector3.Lerp(currentScale, targetScale, Time.deltaTime * scaleTimeFactor);
            }
            else if (Mathf.Abs(planetArrayPosition) == maxPlanetDistance)
            {
                Vector3 targetScale = new Vector3(0.5f, 0.5f, 0.5f);
                planet.transform.localScale = Vector3.Lerp(currentScale, targetScale, Time.deltaTime * scaleTimeFactor);
            }
            else
            {
                Vector3 targetScale = new Vector3(1f, 1f, 1f);
                planet.transform.localScale = Vector3.Lerp(currentScale, targetScale, Time.deltaTime * scaleTimeFactor);
            }
        }
    }
}
