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
        [TextArea(4,8)]
        public string text;


    }
    [System.Serializable]
    public class MyEventType : UnityEvent { }
    [System.Serializable]
    public class Options
    {

        public string answer;
        public MyEventType OnEvent;
    }

    public Info[] dialogueInfo;
    public Options[] dialogueOptions;


    public void loadNextDialogue(DialogueBase nextDialogue)
    {
        DialogueManager.instance.EnqueueDialogue(nextDialogue);
       
    }
}
