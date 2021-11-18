using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartScript : MonoBehaviour
{

    [SerializeField] private GameObject TextObject;
    [SerializeField] private GameObject TextPosition;

    private GameObject instanceOfTextObject;

    // Start is called before the first frame update
    void Start()
    {
        TextObject.GetComponent<TextMesh>().text = "After a long journey you find yourself back on your home planet, but everything changed.\n Find out what happened. It is also not very helpful that you ship ran out of fuel, find some!";
        instanceOfTextObject = Instantiate(TextObject, TextPosition.transform.position, Quaternion.identity, TextPosition.transform);
        instanceOfTextObject.GetComponent<TextMesh>().characterSize = 0.18f;
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
        Destroy(instanceOfTextObject);
        Time.timeScale = 1;

    }
}
