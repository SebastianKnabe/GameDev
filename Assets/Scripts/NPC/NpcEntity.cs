using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcEntity : MonoBehaviour
{

    public DialogueTrigger DialogueTrigger;
    private bool playerInRange;


    void Start(){
        playerInRange = false;
        
    }

    void Update(){
            
            if(Input.GetKeyUp(KeyCode.E) && playerInRange && !DialogueManager.dialogueMode && Time.time >= DialogueManager.timeSinceLastDialogue){

                 DialogueTrigger.TriggerDialogue();
            }
    }

    private void OnTriggerEnter2D(Collider2D other){
    

            if(other.tag == "Player"){
                playerInRange = true;   
        
            }
    }

    private void OnTriggerExit2D(Collider2D other){
    

            if(other.tag == "Player"  ){
            
                playerInRange = false;        
            }
    }

}
