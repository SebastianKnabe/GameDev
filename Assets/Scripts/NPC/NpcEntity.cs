using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcEntity : MonoBehaviour
{
    public GameObject TextObject;
    public GameObject TextPosition;
    public DialogueTrigger DialogueTrigger;

    private bool playerInRange;
    private GameObject instanceOfTextObject;

    void Start()
    {
        playerInRange = false;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && playerInRange && !DialogueManager.dialogueMode && Time.time >= DialogueManager.timeSinceLastDialogue)
        {
            DialogueTrigger.TriggerDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            TextObject.GetComponent<TextMesh>().text = "Press [E] to shop";
            instanceOfTextObject = Instantiate(TextObject, TextPosition.transform.position, Quaternion.identity, TextPosition.transform);
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(instanceOfTextObject);
            playerInRange = false;
        }
    }
}
