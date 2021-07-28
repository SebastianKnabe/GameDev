﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsButton : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;

    public void clickButton()
    {
        if (optionsMenu.active)
        {
            optionsMenu.SetActive(false);
        }
        else
        {
            optionsMenu.SetActive(true);
        }
    }
}