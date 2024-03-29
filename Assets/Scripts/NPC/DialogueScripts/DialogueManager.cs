﻿using System.Collections;
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
    public static bool dialogueMode;

    public float delay;
    public GameObject dialogueOptionUI;
    public GameObject DialogueBox;
    public GameObject choiceButton;
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    public Queue<DialogueBase.Info> dialogueInfo;


    private bool dialogueOptionsMode;
    private bool isTyping;
    private string completedText;
    private GameObject button;
    private DialogueBase currentDialogue;
    private List<GameObject> buttonsCreated;






  

    // Start is called before the first frame update
    void Start()
    {
        DialogueBox.SetActive(false);
        dialogueOptionUI.SetActive(false);
        dialogueInfo = new Queue<DialogueBase.Info>();
        dialogueMode = false;
        dialogueCooldown = 2.05f;
        timeSinceLastDialogue = 0;
        buttonsCreated = new List<GameObject>();
        isTyping = false;
        completedText = "";


    }

    public void EnqueueDialogue (DialogueBase db)
    {
        DialogueBox.SetActive(true);
        dialogueMode = true;
        currentDialogue = db;
        dialogueInfo.Clear();

        



        foreach (DialogueBase.Info info in db.dialogueInfo)
        {
            dialogueInfo.Enqueue(info);
        }
        DequeueDialogue();
       
    }


    public void DequeueDialogue()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = completedText;
            isTyping = false;
            return;
        }

        if (dialogueInfo.Count == 0)
        {
            endDialogue();
            return;
        }



        DialogueBase.Info info = dialogueInfo.Dequeue();
        nameText.text = info.name;
        completedText = info.text;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(info.text));


      

    }

    void endDialogue()
    {
       

      
            dialogueOptionsMode = currentDialogue.dialogueOptions.Length > 0 && dialogueInfo.Count == 0 ? true : false;
            if (dialogueOptionsMode)
            {
                dialogueOptionUI.SetActive(true);

                //EnqueueDialogue(currentDialogue.dialogueOptions[0].nextDialogue);
                for (int i = 0; i < currentDialogue.dialogueOptions.Length; i++)
                {
                    button = Instantiate(choiceButton, dialogueOptionUI.transform);
                    button.GetComponentInChildren<Text>().text = currentDialogue.dialogueOptions[i].answer;
                    int indexParam = i;
                    button.GetComponent<Button>().onClick.AddListener(() => { answerEvent(indexParam); });
                    buttonsCreated.Add(button);
                    button.SetActive(true);


                }



                return;
            }

           
           


            currentDialogue.DialogueEndEvent.Invoke();
            DialogueBox.SetActive(false);
            dialogueMode = false;
            timeSinceLastDialogue = Time.time + dialogueCooldown;



            return;
        }
    

    void answerEvent( int j)
    {
        dialogueOptionUI.SetActive(false);
        dialogueOptionsMode = false;
        
        currentDialogue.dialogueOptions[j].OnEvent.Invoke();


        foreach (GameObject go in buttonsCreated)
        {
            Destroy(go);
        }
        buttonsCreated.Clear();
        




    }



    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char c in sentence.ToCharArray())
        {
            yield return new WaitForSeconds(delay);
            dialogueText.text += c;
            yield return null;
        }
        isTyping = false;
    }


    void Update()
    {


        // render option ui after text typing is finished
        if(!isTyping && dialogueMode && !dialogueOptionsMode && dialogueInfo.Count == 0 && currentDialogue.dialogueOptions.Length > 0)
        {
            
                endDialogue();
                
            
        }

        if (Input.GetKeyDown(KeyCode.E) && dialogueMode && !dialogueOptionsMode)
        {
            
            DequeueDialogue();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && (dialogueMode || dialogueOptionsMode))
        {

            dialogueOptionUI.SetActive(false);
            DialogueBox.SetActive(false);
            dialogueMode = false;
            timeSinceLastDialogue = Time.time + dialogueCooldown;
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
