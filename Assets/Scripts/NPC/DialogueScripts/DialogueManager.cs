using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{

    public static DialogueManager instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("DialogueManager already exists");
        }
        else
        {
            instance = this;
        }
    }


    public static float dialogueCooldown;
    public static float timeSinceLastDialogue;


    public GameObject DialogueBox;
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    public float delay;
    public static bool dialogueMode;
    public Queue<DialogueBase.Info> dialogueInfo;




    // Start is called before the first frame update
    void Start()
    {
        DialogueBox.SetActive(false);
        dialogueInfo = new Queue<DialogueBase.Info>();
        dialogueMode = false;
        dialogueCooldown = 2.05f;
        timeSinceLastDialogue = 0;
        
    }

    public void EnqueueDialogue (DialogueBase db)
    {
        DialogueBox.SetActive(true);
        dialogueMode = true;
        dialogueInfo.Clear();
        foreach(DialogueBase.Info info in db.dialogueInfo)
        {
            dialogueInfo.Enqueue(info);
        }
        DequeueDialogue();
       
    }


    public void DequeueDialogue()
    {

        
        if (dialogueInfo.Count == 0)
        {
            DialogueBox.SetActive(false);
            dialogueMode = false;
            timeSinceLastDialogue = Time.time + dialogueCooldown;
            return;
        }

        DialogueBase.Info info = dialogueInfo.Dequeue();
        nameText.text = info.name;
        //dialogueText.text = info.text;

        Debug.Log("DequeueDialogue");
        Debug.Log("" + info.name);
        Debug.Log("" + info.text);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(info.text));

       


    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char c in sentence.ToCharArray())
        {
            yield return new WaitForSeconds(delay);
            dialogueText.text += c;
            yield return null;
        }
    }


    void Update()
    {


        if (Input.GetKeyDown(KeyCode.E) && dialogueMode)
        {
            DequeueDialogue();
        }

    }

    /*
   


    public void StartDialogue(Dialogue dialogue){


       animator.SetBool("isOpen", true);
       nameText.text = dialogue.name;
   
       sentences.Clear();

       foreach (string sentence in dialogue.sentences){
            
            sentences.Enqueue(sentence);
       }

       DisplayNextSentence();
       dialogueMode = true;
    }

    public void DisplayNextSentence(){

        if(sentences.Count == 0) {

            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        
    }
    
    IEnumerator TypeSentence(string sentence){
        dialogueText.text = "";
        foreach(char c in sentence.ToCharArray()){
            dialogueText.text += c;
            yield return null;
        }
    }

    void EndDialogue(){
        
        animator.SetBool("isOpen", false);
        dialogueMode = false;
        timeSinceLastDialogue = Time.time + dialogueCooldown;
        
    }
      */
}
