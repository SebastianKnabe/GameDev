using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{

    public int sceneIndex;
   
    
    private bool playerInRange;


    // Update is called once per frame

    void Start()
    {
        playerInRange = false;

    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && playerInRange && !DialogueManager.dialogueMode)
        {

           SceneManager.LoadScene(sceneIndex);
            //GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");
           // Debug.Log(doors);
            //GameObject player = GameObject.Find("Player");

            //player.transform.position = doors[0].transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {

            playerInRange = true;

        }
    }

}
