using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    [SerializeField] private GameObject exitGameMenu;
    [SerializeField] private VoidEvent saveGameEvent;

    public void ActivateExitGameMenu()
    {
        exitGameMenu.SetActive(true);
    }

    public void DeactivateExitGameMenu()
    {
        exitGameMenu.SetActive(false);
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game " + saveGameEvent);
        saveGameEvent.Raise();
        Application.Quit();
    }
}
