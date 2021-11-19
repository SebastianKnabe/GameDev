using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlanetHolder : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI planetNameTextholder;
    [SerializeField] private TextMeshProUGUI planetFlavourTextholder;
    [SerializeField] private GameSettings gameSettings;

    private int currentSelectedPlanet;
    private int numberOfPlanets;
    private int maxPlanetArrayDistance;
    private int[] planetSelectionPosition;
    private float sumPlanetDistance;
    private float planetMoveFactor = 50f;
    private float scaleTimeFactor = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        int lastScene = PlayerPrefs.GetInt("LastScene");
        if (lastScene == 0)
        {
            //Todo Start Animation
            PlayerPrefs.SetInt("LastScene", SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene(gameSettings.lastPlayerScene);
        }
        else
        {
            currentSelectedPlanet = lastScene - 2;
            numberOfPlanets = gameObject.transform.childCount;
            maxPlanetArrayDistance = numberOfPlanets / 2;
            planetSelectionPosition = new int[numberOfPlanets];

            float oldMoveFactor = planetMoveFactor;
            float oldScaleFactor = scaleTimeFactor;

            planetMoveFactor = 1f;
            scaleTimeFactor = 100f;
            DeterminPlanetArrayPosition();
            SetPlanetPositions();
            scaleTimeFactor = oldScaleFactor;
            planetMoveFactor = oldMoveFactor;
        }
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
            DeterminPlanetArrayPosition();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            currentSelectedPlanet += 1;
            if (currentSelectedPlanet >= numberOfPlanets)
            {
                currentSelectedPlanet = 0;
            }
            DeterminPlanetArrayPosition();
        }
        else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E))
        {
            if (sumPlanetDistance < 100f)
            {
                GameObject planet = gameObject.transform.GetChild(currentSelectedPlanet).gameObject;
                planet.GetComponent<PlanetSelection>().selectPlanet();
            }
        }
        SetPlanetPositions();
    }

    private void DeterminPlanetArrayPosition()
    {
        for (int i = 0; i < numberOfPlanets; i++)
        {
            //Bestimme Planeten Position im Kreis
            int planetArrayPosition = i - currentSelectedPlanet;

            if (planetArrayPosition > maxPlanetArrayDistance)
            {
                //Debug.Log("i: " + i + "\nplanetarrayPosition: " + planetArrayPosition);
                planetArrayPosition = -3 + planetArrayPosition - maxPlanetArrayDistance;
            }
            else if (planetArrayPosition < -maxPlanetArrayDistance)
            {
                //Debug.Log("i: " + i + "\nplanetarrayPosition: " + planetArrayPosition);
                planetArrayPosition = 3 - Mathf.Abs(planetArrayPosition) + maxPlanetArrayDistance;
            }

            planetSelectionPosition[i] = planetArrayPosition;
        }

        ChangeClickablePlanet();
    }

    private void SetPlanetPositions()
    {
        sumPlanetDistance = 0;
        for (int i = 0; i < numberOfPlanets; i++)
        {
            //Hole Planeten Position im Kreis
            int planetArrayPosition = planetSelectionPosition[i];

            //Ermittle Position
            float xPosition = planetArrayPosition * 400;
            float yPosition = Mathf.Abs(planetArrayPosition) * 2 * 100f;
            if (Mathf.Abs(planetArrayPosition) == maxPlanetArrayDistance)
            {
                xPosition = 60 * planetArrayPosition;
                yPosition = 400;
            }

            //Planet-Gameobject
            GameObject planet = gameObject.transform.GetChild(i).gameObject;

            //Bewege zu Position
            Vector3 targetPosition = new Vector3(xPosition, yPosition, 0f);
            Vector3 currentPosition = planet.transform.localPosition;
            float positionDistance = (currentPosition - targetPosition).magnitude;
            sumPlanetDistance += positionDistance;
            planet.transform.localPosition = Vector3.MoveTowards(currentPosition, targetPosition, positionDistance / planetMoveFactor);

            //Skaliere Planeten -> Vordergrund größer, Hintergrund kleiner
            Vector3 currentScale = planet.transform.localScale;
            if (planetArrayPosition == 0)
            {
                Vector3 targetScale = new Vector3(1.5f, 1.5f, 1.5f);
                planet.transform.localScale = Vector3.Lerp(currentScale, targetScale, Time.deltaTime * scaleTimeFactor);
            }
            else if (Mathf.Abs(planetArrayPosition) == maxPlanetArrayDistance)
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

    private void ChangeClickablePlanet()
    {
        for (int i = 0; i < numberOfPlanets; i++)
        {
            int planetArrayPosition = planetSelectionPosition[i];
            GameObject planet = gameObject.transform.GetChild(i).gameObject;
            Animator planetAnimator = planet.GetComponent<Animator>();
            if (planetArrayPosition == 0)
            {
                planet.GetComponent<Button>().interactable = true;
                planet.GetComponent<Canvas>().sortingOrder = 3;

                //Edit header and flavour text
                PlanetSelection planetScript = planet.GetComponent<PlanetSelection>();
                planetNameTextholder.text = planetScript.getPlanetName();
                planetFlavourTextholder.text = planetScript.getFlavourText();

                if (planetAnimator != null)
                {
                    planetAnimator.enabled = true;
                }
            }
            else
            {
                planet.GetComponent<Button>().interactable = false;
                if (Mathf.Abs(planetArrayPosition) == 1)
                {
                    planet.GetComponent<Canvas>().sortingOrder = 2;
                }
                else
                {
                    planet.GetComponent<Canvas>().sortingOrder = 1;
                }
                if (planetAnimator != null)
                {
                    planetAnimator.enabled = false;
                }
            }
        }
    }
}
