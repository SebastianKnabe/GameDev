using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{

    public int sceneIndex;
    public string connectedDoor;
    public string spawnId;

    private int inital_sceneIndex;
    private string inital_connectedDoor;
    private string inital_spawnId;


    private bool playerInRange;


    // Update is called once per frame

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject.Find("SpawnManagerObject").GetComponent<SpawnManager>().onLevelLoaded();
    }

    void Start()
    {
        playerInRange = false;
        inital_sceneIndex = sceneIndex;
        inital_connectedDoor = connectedDoor;
        inital_spawnId = spawnId;

    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && playerInRange && !DialogueManager.dialogueMode)
        {
            Debug.Log("CURRENT conn " + connectedDoor);


            //player.spawnPoint = connectedDoor;

            //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEntity>().setSpawnPoint(connectedDoor);

            // Debug.Log("CURRENT conn2 " + GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEntity>().getSpawnPoint());
            GameObject.Find("SpawnManagerObject").GetComponent<SpawnManager>().spawnPoint = connectedDoor;
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

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.tag == "Player")
        {

            playerInRange = false;

        }
    }


    public void changeSpawnPoint(string newSpawn)
    {
        connectedDoor = newSpawn;
    }

    public void resetAllSettings()
    {
        sceneIndex = inital_sceneIndex;
        connectedDoor = inital_connectedDoor;
        spawnId = inital_spawnId;
    }


}
