using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
	public DialogueBase dialogue;

	public void TriggerDialogue(){

		DialogueManager.instance.EnqueueDialogue(dialogue);
	}

}
