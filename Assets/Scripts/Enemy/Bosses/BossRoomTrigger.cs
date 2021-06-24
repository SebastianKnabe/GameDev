using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
    public GameObject BossUI;
    public AudioSource audioSource;

    [SerializeField] private AudioClip bossMusic;

    private bool fightStarted = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !fightStarted)
        {
            BossUI.SetActive(true);

            audioSource.clip = bossMusic;
            audioSource.Play();

            gameObject.GetComponent<CircleCollider2D>().enabled = false;

            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void EndBossFight()
    {
        BossUI.SetActive(false);
        audioSource.Stop();
        Destroy(gameObject);
    }
}
