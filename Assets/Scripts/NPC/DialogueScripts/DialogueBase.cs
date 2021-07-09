using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogues")]
public class DialogueBase : ScriptableObject
{
 
    [System.Serializable]
    public class Info
    {

        public string name;
        [TextArea(4, 8)]
        public string text;
    }
    //@TODO find a way to check if a unityevent has no function assigned to invoke 
    public UnityEvent DialogueEndEvent;

    [System.Serializable]
    public class Options
    {

        public string answer;
        public UnityEvent OnEvent;
    }

    public Info[] dialogueInfo;
    public Options[] dialogueOptions;


    public void loadNextDialogue()
    {
        DialogueManager.instance.EnqueueDialogue(this);
       
    }
}
