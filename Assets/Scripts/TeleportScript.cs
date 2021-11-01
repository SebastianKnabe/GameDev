using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{

    [Header("Basic")]
    [SerializeField] private VoidEvent playTeleportedEvent;

    [Header("Teleport position")]
    [SerializeField] private Transform teleportPos;
    [SerializeField] private GameObject camera;

    private bool hasCooldown = false;
    private IEnumerator coroutine;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !hasCooldown)
        {
            collision.gameObject.transform.position = new Vector3(teleportPos.position.x, teleportPos.position.y, teleportPos.position.z);
            camera.transform.position = new Vector3(teleportPos.position.x, teleportPos.position.y, camera.transform.position.z);
            playTeleportedEvent.Raise();
         

        }
    }

    public void PlayerTeleported()
    {
        hasCooldown = true;
        coroutine = WaitForCooldown();
        StartCoroutine(coroutine);
    }

    private IEnumerator WaitForCooldown()
    {
        yield return new WaitForSeconds(2f);
        hasCooldown = false;

    }
}
