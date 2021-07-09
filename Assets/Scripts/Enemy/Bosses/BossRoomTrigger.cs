using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
    public GameObject BossUI;
    public AudioSource BGMAudioSource;

    [SerializeField] private AudioClip bossMusic;

    private bool fightStarted = false;
    private AudioClip oldClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !fightStarted)
        {
            BossUI.SetActive(true);

            oldClip = BGMAudioSource.clip;
            BGMAudioSource.clip = bossMusic;
            BGMAudioSource.Play();

            gameObject.GetComponent<CircleCollider2D>().enabled = false;

            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void EndBossFight()
    {
        BossUI.SetActive(false);
        BGMAudioSource.Stop();
        BGMAudioSource.clip = oldClip;
        BGMAudioSource.Play();
        Destroy(gameObject);
    }
}
