using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScreen : MonoBehaviour
{
    private bool isGamePaused = false;
    public GameObject mainGameUI;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }    
    }

    void Resume ()
    {
        Time.timeScale = 1f;
        isGamePaused = false;
        mainGameUI.SetActive(true);
        player.SetActive(true);
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        isGamePaused = true;
        mainGameUI.SetActive(false);
        player.SetActive(false);
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
