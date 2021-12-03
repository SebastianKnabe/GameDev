using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpikeSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip spikeSound;

    public void PlaySpikeSound()
    {
        audioSource.PlayOneShot(spikeSound);
    }
}
