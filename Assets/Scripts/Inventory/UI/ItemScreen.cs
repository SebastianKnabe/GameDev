using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScreen : MonoBehaviour
{
    private bool isGamePaused = false;
    public GameObject mainGameUI;
    public GameObject player;
    public GameObject targetInventoryPanel;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    //Unpause and close Inventory
    void Resume()
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

    //Pause Game and open Inventory
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
        targetInventoryPanel.SetActive(false);
    }

    public void showTargetInventoryPanel()
    {
        Pause();
        targetInventoryPanel.SetActive(true);
    }
}
