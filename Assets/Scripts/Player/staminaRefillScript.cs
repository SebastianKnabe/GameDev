using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class staminaRefillScript : MonoBehaviour
{

    public Image staminaUI;
    public bool playerSprint;

    private float waitTime;
    private float regainAmount;
    private float decreaseAmount;
    private bool hitZero;

    // Start is called before the first frame update
    void Start()
    {
        staminaUI = GetComponent<Image>();
        regainAmount = 5.0f;
        decreaseAmount = 10.0f;
        waitTime = 30.0f;
        playerSprint = false;
        hitZero = false;
    }

    // Update is called once per frame
    void Update()
    {
        //cooldown
        if (hitZero)
        {
            staminaUI.color = new Color32(255, 0, 0, 255);

            if (staminaUI.fillAmount >= 0.3f)
            {
                hitZero = false;
                staminaUI.color = new Color32(255, 255, 255, 255);
            }
        }

        if (!playerSprint && staminaUI.fillAmount < 100)
        {
            staminaUI.fillAmount += (regainAmount) / waitTime * Time.deltaTime;
        }
        else if (!hitZero && playerSprint && staminaUI.fillAmount > 0)
        {
            staminaUI.fillAmount -= (decreaseAmount) / waitTime * Time.deltaTime;
        }

        if (staminaUI.fillAmount <= 0)
        {
            hitZero = true;
        }
    }

    public bool requestSprint(bool value)
    {
        playerSprint = value ? !hitZero : value;
        return playerSprint;
    }

}
