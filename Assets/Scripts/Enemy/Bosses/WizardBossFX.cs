using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardBossFX : MonoBehaviour
{
    [SerializeField] private AudioClip teleportSoundFX;
    [SerializeField] private AudioClip meteorSoundFX;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Camera mainCamera;

    private Color32 defaultBackground;

    private void Start()
    {
        defaultBackground = mainCamera.backgroundColor;
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
    }

    public void turnBackgroundNormal()
    {
        mainCamera.backgroundColor = defaultBackground;
    }
}
