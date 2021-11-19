using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class WizardBossFX : MonoBehaviour
{
    [SerializeField] private AudioClip teleportSoundFX;
    [SerializeField] private AudioClip meteorSoundFX;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Light2D lighting;



    private Color32 defaultBackground;
    private Color32 defaultLight;

    private void Start()
    {
        defaultBackground = mainCamera.backgroundColor;
        defaultLight = lighting.color;
    }


    public void playTeleportSound()
    {
        audioSource.PlayOneShot(teleportSoundFX);
    }

    public void playMeteorSound()
    {
        audioSource.PlayOneShot(meteorSoundFX);
    }

    public void turnBackgroundRed()
    {
        mainCamera.backgroundColor = new Color32(125, 28, 33, 255);
        lighting.color = new Color32(125, 28, 33, 255);
    }

    public void turnBackgroundNormal()
    {
        mainCamera.backgroundColor = defaultBackground;
        lighting.color = defaultLight;
    }
}
