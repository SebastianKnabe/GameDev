using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpikeSound : MonoBehaviour
{

    public AudioSource audioSource;
    [SerializeField] private AudioClip spikeSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlaySpikeSound()
    {
        audioSource.PlayOneShot(spikeSound);
    }
}
