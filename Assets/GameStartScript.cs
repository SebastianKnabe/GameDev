using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStartScript : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI textObject;

    private GameObject instanceOfTextObject;

    // Start is called before the first frame update
    void Start()
    {
        textObject.SetText("After a long journey you find yourself back on your home planet, but everything changed. Find out what happened. It is also not very helpful that you ship ran out of fuel, find some!");
        StartCoroutine(showStartText());

    }

    IEnumerator showStartText()
    {
        Time.timeScale = 0;
        float pauseEndTime = Time.realtimeSinceStartup + 5f;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;

        }
        textObject.gameObject.SetActive(false);
        Time.timeScale = 1;

    }
}
